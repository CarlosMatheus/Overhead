using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour 
{
    [SerializeField] private GameObject waveWarning = null;
    [SerializeField] private GameObject prepareYourself = null;

    private GameObject waveCanvas;
    private GameObject waveCooldown;
    private GameObject InitialAnimation;
    private GameObject tutorialNameCanvas;
    private GameObject canvas;

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

    public void SetWaveCanvasAlphaWithDelay(float delay)
    {
        Invoke("SetWaveCanvasAlpha", delay);
    }

    public void SetWaveCoolDownAlphaWithDelay(float delay)
    {
        Invoke("SetCanvasAfterAnimation", delay);
    }

    public void SetWaveCanvasAlpha(float val = 1f)
    {
        waveCanvas.GetComponent<CanvasGroup>().alpha = val;
    }

    public void SetWaveCoolDownAlpha(float val = 1f)
    {
        waveCooldown.GetComponent<CanvasGroup>().alpha = val;
    }

    public void SetTutorialNameCanvasAlpha(float val = 1f)
    {
        tutorialNameCanvas.GetComponent<CanvasGroup>().alpha = val;
    }

    public void AppearWaveCanvas()
    {
        waveCanvas.GetComponent<Animation>().Play("Appear");
    }

    public void DisappearWaveCanvas()
    {
        waveCanvas.GetComponent<Animation>().Play("Disappear");
    }

    public void AppearWaveCoolDown()
    {
        waveCooldown.GetComponent<Animation>().Play("Appear");
    }

    public void DisappearWaveCoolDown()
    {
        waveCooldown.GetComponent<Animation>().Play("Disappear");
    }

    public void AppearTutorialNameCanvas()
    {
        tutorialNameCanvas.GetComponent<Animation>().Play("Appear");
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
        InitialAnimation.SetActive(false);
        waveCanvas.GetComponent<CanvasGroup>().alpha = 0;
        waveCooldown.GetComponent<CanvasGroup>().alpha = 0;
        tutorialNameCanvas.GetComponent<CanvasGroup>().alpha = 0;
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
        waveCanvas = GameObject.Find("WaveCanvas");
        waveCooldown = GameObject.Find("WaveCooldown");
        InitialAnimation = GameObject.Find("InitialLoading");
        tutorialNameCanvas = GameObject.Find("TutorialNameCanvas");
    }
}
