using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blur : MonoBehaviour {

    private Material _blurMaterial;

    private void OnEnable()
    {
        _blurMaterial = new Material(Shader.Find("Hidden/Blur"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 0);

        Graphics.Blit(source, rt, _blurMaterial);

        for (int i = 0; i < 10; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
            Graphics.Blit(rt, rt2, _blurMaterial);
            Graphics.Blit(rt2, rt, _blurMaterial);
            RenderTexture.ReleaseTemporary(rt2);
        }

        Graphics.Blit(rt, destination);

        rt.Release();
    }
}
