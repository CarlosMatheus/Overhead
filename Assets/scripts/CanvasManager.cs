using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    [SerializeField] private GameObject waveWarning = null;
    [SerializeField] private GameObject prepareYourself = null;

    public void PlayWaveWarning()
    {
        waveWarning.GetComponent<Animation>().Play("FadeWarningCanvas");
    }

    public void PlayPrepareYourSelf()
    {
        prepareYourself.GetComponent<Animation>().Play("FadeWarningCanvas");
    }

}
