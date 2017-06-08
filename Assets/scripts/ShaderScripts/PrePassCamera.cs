using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PrePassCamera : MonoBehaviour {

    private static RenderTexture PrePass;     //Texture where the solid color is stored
    private static RenderTexture BlurTexture; //Texture where the blur is stored

    private Material _blurMaterial; //To add the blur effect

    private void OnEnable()
    {
        PrePass = new RenderTexture(Screen.width, Screen.height, 24); 
        BlurTexture = new RenderTexture(Screen.width >> 1, Screen.height >> 1, 0); //Downing the resolution for the blur

        Camera _camera = GetComponent<Camera>();
        Shader outlineShader = Shader.Find("Hidden/OutlineReplacement"); //Getting the solid color shader

        _camera.targetTexture = PrePass; //Makes the camera render in the solid color texture
        _camera.SetReplacementShader(outlineShader, "Glowable");  //Rendering with the shader 

        Shader.SetGlobalTexture("_PrePassTex", PrePass); //Passing the solid color texture to the composite shader
        Shader.SetGlobalTexture("_BlurTex", BlurTexture); //Passing the blur texture to the composite shader

        _blurMaterial = new Material(Shader.Find("Hidden/Blur")); //To apply the blur shader later on
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination); //Destination only receives the solid color

        Graphics.SetRenderTarget(PrePass);

        Graphics.Blit(source, BlurTexture); //Time to apply the blur. First, the blurTexture is merged with what the camera is seeing

        for (int i = 0 ; i < 10; i++)
        {
            RenderTexture rt = RenderTexture.GetTemporary(BlurTexture.width, BlurTexture.height); //Temporary texture to apply iterative blur
            Graphics.Blit(BlurTexture, rt, _blurMaterial); //Step 1 of iterative blur, the temporary texture receives the blur texture, but blurred(by the blur shader)
            Graphics.Blit(rt, BlurTexture, _blurMaterial); //Step 2 of iterative blur, now the blurtexture receives its blurred version and it gets blurred one more time in the process
            RenderTexture.ReleaseTemporary(rt);
        }
        //After the loop, the blur texture is very blurred
    }
}
