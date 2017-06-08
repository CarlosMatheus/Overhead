using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CompositeScript : MonoBehaviour {

    public float Intensity = 2;

    private Material _compositeMaterial;

    private void OnEnable()
    {
        _compositeMaterial = new Material(Shader.Find("Hidden/CompositeShader")); //Gets the shader to merge the two renderers
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _compositeMaterial.SetFloat("_Intensity", Intensity); //Setting the intensity of the glow effect
        Graphics.Blit(source, destination, _compositeMaterial, 0); //Adding the shader to merge
    }
}
