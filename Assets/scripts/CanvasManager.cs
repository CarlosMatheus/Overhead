using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    [SerializeField] private GameObject waveWarning = null;
    [SerializeField] private GameObject prepareYourself = null;

    private GameObject canvas;
    private CanvasGroup waveCanvas;
    private CanvasGroup waveCooldown;
    private GameObject InitialAnimation;

    public void PlayWaveWarning()
    {
        waveWarning.GetComponent<Animation>().Play("FadeWarningCanvas");
    }

    public void PlayPrepareYourSelf()
    {
        prepareYourself.GetComponent<Animation>().Play("FadeWarningCanvas");
    }

    public void PlayInitialLoadingAnimation()
    {
        InitialAnimation.GetComponent<Animation>().Play("FadeWarningCanvas");
    }

    public void SetWaveCanvasAlpha(float val)
    {
        waveCanvas.alpha = val;
    }

    public void SetWaveCoolDownAlpha(float val)
    {
        waveCooldown.alpha = val;
    }

    public void SetCanvasAlpha(float val)
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).GetComponent<CanvasGroup>().alpha = val;
        }
    }

    public void PlayAppearCanvasWithDelay(float delay)
    {
        Invoke("PlayAppearCanvas", delay);
    }

    public void SetCanvasAfterAnimationWithDelay(float delay)
    {
        Invoke("SetCanvasAfterAnimation", delay);
    }

    public void SetCanvasAfterAnimation()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        SetCanvasAlpha(1f);
    }

    public void PlayAppearCanvas()
    {
        canvas.GetComponent<Animation>().Play("Appear");
    }

    public void PlayDisappearCanvas()
    {
        canvas.GetComponent<Animation>().Play("Disappear");
    }

    private void Awake()
    {
        canvas = GameObject.FindWithTag("Canvas");
        waveCanvas = GameObject.Find("WaveCanvas").GetComponent<CanvasGroup>();
        waveCooldown = GameObject.Find("WaveCooldown").GetComponent<CanvasGroup>();
        InitialAnimation = GameObject.Find("InitialLoading");
    }
}
