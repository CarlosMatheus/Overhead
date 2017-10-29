using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using System.IO;

public class AudioRecorder : MonoBehaviour {

	bool isRecording = false;
	private AudioSource audioSource;

	//temporary audio vector we write to every second while recording is enabled..
	List<float> tempRecording = new List<float>();

	//list of recorded clips...
	List<float[]> recordedClips = new List<float[]>();

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		//set up recording to last a max of 1 seconds and loop over and over
		//audioSource.clip = Microphone.Start(null, true, 1, 44100);
		//audioSource.Play();
		//resize our temporary vector every second
		//Invoke("ResizeRecording", 1);
		UnityEngine.Debug.Log("Para gravar um som, aperte espaço. Este será salvo enquanto o jogo estiver em execução. Para ouvir os sons gravados, navegue\n pelas teclas númericas e aperte E caso deseje salvar o som gravado em execução. O arquivo será salvo em (currentdirectory)\\audio\\audio.wav");
	}

	void ResizeRecording()
	{
		if (isRecording)
		{
			//add the next second of recorded audio to temp vector
			int length = 44100;
			float[] clipData = new float[length];
			audioSource.clip.GetData(clipData, 0);
			tempRecording.AddRange(clipData);
			Invoke("ResizeRecording", 1);
		}
	}

	void Update()
	{
		//space key triggers recording to start...
		if (Input.GetKeyDown("space"))
		{
			isRecording = !isRecording;
			UnityEngine.Debug.Log(isRecording == true ? "Is Recording" : "Off");

			if (isRecording == false)
			{
				//stop recording, get length, create a new array of samples
				int length = Microphone.GetPosition(null);

				Microphone.End(null);
				float[] clipData = new float[length];
				audioSource.clip.GetData(clipData, 0);

				//create a larger vector that will have enough space to hold our temporary
				//recording, and the last section of the current recording
				float[] fullClip = new float[clipData.Length + tempRecording.Count];
				for (int i = 0; i < fullClip.Length; i++)
				{
					//write data all recorded data to fullCLip vector
					if (i < tempRecording.Count)
						fullClip[i] = tempRecording[i];
					else
						fullClip[i] = clipData[i - tempRecording.Count];
				}

				recordedClips.Add(fullClip);
				audioSource.clip = AudioClip.Create("recorded samples", fullClip.Length, 1, 44100, false);
				audioSource.clip.SetData(fullClip, 0);
				audioSource.loop = true;

			}
			else
			{
				//stop audio playback and start new recording...
				audioSource.Stop();
				tempRecording.Clear();
				Microphone.End(null);
				audioSource.clip = Microphone.Start(null, true, 1, 44100);
				Invoke("ResizeRecording", 1);
			}

		}

		//use number keys to switch between recorded clips, start from 1!!
		for (int i = 0; i < 10; i++)
		{
			if (Input.GetKeyDown("" + i))
			{
				SwitchClips(i - 1);
			}
		}

		// Press E to save the current recorded clip playing on audiosource (the last one by default)
		if (Input.GetKeyDown(KeyCode.E))
		{
			GenerateAudio.CallSaveFunction("audio", audioSource.clip);
		}

	}

	//chooose which clip to play based on number key..
	void SwitchClips(int index)
	{
		if (index < recordedClips.Count)
		{
			audioSource.Stop();
			int length = recordedClips[index].Length;
			audioSource.clip = AudioClip.Create("recorded samples", length, 1, 44100, false);
			audioSource.clip.SetData(recordedClips[index], 0);
			audioSource.loop = true;
			audioSource.Play();
		}
	}
}

public static class GenerateAudio
{

	public static void CallSaveFunction(string fileName, AudioClip clip)
	{
		SavingStuff.Save(fileName, clip);
	}

}

public static class SavingStuff
{
	const int HEADER_SIZE = 44;

	public static bool Save(string filename, AudioClip clip)
	{
		if (!filename.ToLower().EndsWith(".wav"))
		{
			filename += ".wav";
		}

		var filepath = Path.Combine(System.IO.Directory.GetCurrentDirectory() + "\\audio", filename);

		UnityEngine.Debug.Log(filepath);

		// Make sure directory exists if user is saving to sub dir.
		Directory.CreateDirectory(Path.GetDirectoryName(filepath));

		using (var fileStream = CreateEmpty(filepath))
		{

			ConvertAndWrite(fileStream, clip);

			WriteHeader(fileStream, clip);
		}

		return true; // TODO: return false if there's a failure saving the file
	}

	public static AudioClip TrimSilence(AudioClip clip, float min)
	{
		var samples = new float[clip.samples];

		clip.GetData(samples, 0);

		return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);
	}

	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz)
	{
		return TrimSilence(samples, min, channels, hz, false, false);
	}

	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream)
	{
		int i;

		for (i = 0; i < samples.Count; i++)
		{
			if (Mathf.Abs(samples[i]) > min)
			{
				break;
			}
		}

		samples.RemoveRange(0, i);

		for (i = samples.Count - 1; i > 0; i--)
		{
			if (Mathf.Abs(samples[i]) > min)
			{
				break;
			}
		}

		samples.RemoveRange(i, samples.Count - i);

		var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);

		clip.SetData(samples.ToArray(), 0);

		return clip;
	}

	static FileStream CreateEmpty(string filepath)
	{
		var fileStream = new FileStream(filepath, FileMode.Create);
		byte emptyByte = new byte();

		for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
		{
			fileStream.WriteByte(emptyByte);
		}

		return fileStream;
	}

	static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
	{

		var samples = new float[clip.samples];

		clip.GetData(samples, 0);

		Int16[] intData = new Int16[samples.Length];

		Byte[] bytesData = new Byte[samples.Length * 2];

		const int rescaleFactor = 32767;

		for (int i = 0; i < samples.Length; i++)
		{
			intData[i] = (short)(samples[i] * rescaleFactor);
			Byte[] byteArr = new Byte[2];
			byteArr = BitConverter.GetBytes(intData[i]);
			byteArr.CopyTo(bytesData, i * 2);
		}

		Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
		fileStream.Write(bytesData, 0, bytesData.Length);
	}

	static void WriteHeader(FileStream fileStream, AudioClip clip)
	{

		var hz = clip.frequency;
		var channels = clip.channels;
		var samples = clip.samples;

		fileStream.Seek(0, SeekOrigin.Begin);

		Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(riff, 0, 4);

		Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
		fileStream.Write(chunkSize, 0, 4);

		Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(wave, 0, 4);

		Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(fmt, 0, 4);

		Byte[] subChunk1 = BitConverter.GetBytes(16);
		fileStream.Write(subChunk1, 0, 4);

		UInt16 two = 2;
		UInt16 one = 1;

		Byte[] audioFormat = BitConverter.GetBytes(one);
		fileStream.Write(audioFormat, 0, 2);

		Byte[] numChannels = BitConverter.GetBytes(channels);
		fileStream.Write(numChannels, 0, 2);

		Byte[] sampleRate = BitConverter.GetBytes(hz);
		fileStream.Write(sampleRate, 0, 4);

		Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
		fileStream.Write(byteRate, 0, 4);

		UInt16 blockAlign = (ushort)(channels * 2);
		fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

		UInt16 bps = 16;
		Byte[] bitsPerSample = BitConverter.GetBytes(bps);
		fileStream.Write(bitsPerSample, 0, 2);

		Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
		fileStream.Write(datastring, 0, 4);

		Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
		fileStream.Write(subChunk2, 0, 4);

		//		fileStream.Close();
	}
}
