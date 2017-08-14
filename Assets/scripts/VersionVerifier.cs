using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionVerifier : MonoBehaviour {

    private const string privateCode = "rPbePkYpa0qicjfJIt11CAPsdXAptREkKhp_twQJg_uA";
    private const string publicCode = "5991b46c958b92190ce7c835";
    private const string webURL = "http://dreamlo.com/lb/";

    private GameObject versionAlertCanvas;
    private string versionOfThisGame;
    private string versionOnServer;
    private bool isUpdated;

    public void OkButton()
    {
        versionAlertCanvas.SetActive(false);
    }

    private void Start()
    {
        versionAlertCanvas = GameObject.Find("VersionAlertCanvas");
        versionAlertCanvas.SetActive(false);
        versionOfThisGame = GameObject.Find("_VERSION").GetComponent<_Version>().GetVersion();
        DownloadVersion();
    }

    private void DownloadVersion()
    {
        StartCoroutine("DownloadVersionFromDatabase");
    }

    private void VerifyVersion(string _version)
    {
        versionOnServer = _version;
        isUpdated = string.Equals(versionOnServer,versionOfThisGame);
        Debug.Log(isUpdated);
        if (isUpdated == false)
        {
            AppearAlert();
        }
        else
        {
            return;
        }
    }

    private void AppearAlert()
    {
        versionAlertCanvas.SetActive(true);
        versionAlertCanvas.GetComponent<Animator>().Play("VersionAlert");
    }

    private string AjustText(string _text)
    {
        Debug.Log(_text.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries)[0]);
        return _text.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
    }

    IEnumerator DownloadVersionFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if(string.IsNullOrEmpty(www.error))
        {
            yield return new WaitForSeconds(0.5f);
            VerifyVersion( AjustText(www.text) );
        }
        else
        {
            Debug.Log("Erro:" + www.error);
        }
    }

}
