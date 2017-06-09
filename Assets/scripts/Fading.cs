using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;
	public int fadeDir = -1;         

	private int drawDepth = -1000;   //the layer of the texture
	private float alpha = 1.0f;      //the a alpha of the texture

	void OnGUI(){

		alpha += fadeDir * fadeSpeed * Time.deltaTime; 

		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
//		GUI.DrawTexture (new Rect (0,0,Screen.width, Screen.height));
	}

	public float Beginfade(int direction){
		fadeDir = direction;
		return (fadeSpeed); 
	}

}
