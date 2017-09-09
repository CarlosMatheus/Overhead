using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVerifier : MonoBehaviour 
{
    private const string tutorialKey = "PlayedTutorial";
    private GameObject tutorialAlertCanvas;
    private bool playedTutorial;
    private _Version version;
	
    public void PlayTutorial()
    {
        PlayerPrefs.SetString(tutorialKey, version.GetVersion() );
        playedTutorial = true;
        PlayerPrefs.Save();
    }

    public bool GetPlayedTutorial()
    {
        return playedTutorial;
    }

    public void AppearTutorialCanvas()
    {
        tutorialAlertCanvas.SetActive(true);
        tutorialAlertCanvas.GetComponent<Animator>().Play("VersionAlert");
    }

    private void Start()
    {
        tutorialAlertCanvas = GameObject.Find("TutorialAlertCanvas");
        tutorialAlertCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        tutorialAlertCanvas.SetActive(false);
        playedTutorial = PlayerPrefs.HasKey(tutorialKey);
        version = GameObject.Find("_VERSION").GetComponent<_Version>();
    }

    private void Update()
    {
        if( Input.GetKeyDown("k") )
        {
            PlayerPrefs.DeleteKey(tutorialKey);
            Debug.LogWarning("Deleted key: " + tutorialKey);
            PlayerPrefs.Save();
            playedTutorial = PlayerPrefs.HasKey(tutorialKey);
        }
    }
}
