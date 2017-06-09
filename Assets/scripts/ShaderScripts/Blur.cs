using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blur : MonoBehaviour {

    public int NumberOfIterations = 10;
    public int Resolution = 0;

    private Material _blurMaterial;

    private void OnEnable()
    {
        _blurMaterial = new Material(Shader.Find("Hidden/Blur"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = Screen.width >> Resolution;
        int height = Screen.height >> Resolution;

        RenderTexture rt = new RenderTexture(width, height, 0);

        Graphics.Blit(source, rt, _blurMaterial);

        for (int i = 0; i < NumberOfIterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height, 0);
            Graphics.Blit(rt, rt2, _blurMaterial);
            Graphics.Blit(rt2, rt, _blurMaterial);
            RenderTexture.ReleaseTemporary(rt2);
        }

        Graphics.Blit(rt, destination);

        rt.Release();
    }
}
