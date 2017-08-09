using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;
	public int fadeDir = -1;         

	private int drawDepth = -1000;   //the layer of the texture
	private float alpha = 1.0f;      //the a alpha of the texture
    private LeaderBoardControllerScript leaderBoardControllerScript;

    public void OnGUI(){
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

	public void AppearPlayerScoreCanvas(){
        leaderBoardControllerScript.SetPlayerScoreCanvasActive(true);
		GameObject gObj = GameObject.Find ("PlayerScoreCanvas");
		StartCoroutine (FadeCanvasComponents(1,gObj));
	}

	public void DisappearPlayerScoreCanvas(){
		GameObject gObj = GameObject.Find ("PlayerScoreCanvas");
		StartCoroutine (FadeCanvasComponents(-1,gObj));
	}

	public void AppearConnectionErrorMessageCanvas(){
        leaderBoardControllerScript.SetConnectionErrorCanvas(true);
        GameObject gObj = GameObject.Find ("ConnectionErrorCanvas");
		StartCoroutine (FadeCanvasComponents(1,gObj));
	}

	public void DisappearConnectionErrorMessageCanvas(){
		GameObject gObj = GameObject.Find ("ConnectionErrorCanvas");
		StartCoroutine (FadeCanvasComponents(-1,gObj));
	}

	public void AppearOfflineScoreCanvas(){
        leaderBoardControllerScript.SetOfflineScoreCanvas(true);
		GameObject gObj = GameObject.Find ("OfflineScoreCanvas");
		StartCoroutine (FadeCanvasComponents(1,gObj));
	}
		
	public void AppearLeaderBoardCanvas(){
        leaderBoardControllerScript.SetleaderBoadCanvas(true);
		GameObject gObj = GameObject.Find ("LeaderBoadCanvas");
		StartCoroutine (FadeCanvasComponents(1,gObj));
	}

	public void AppearLeaderBoadCanceledCanvas(){
        leaderBoardControllerScript.SetleaderBoadCanceledCanvas(true);
		GameObject gObj = GameObject.Find ("LeaderBoadCanceledCanvas");
		StartCoroutine (FadeCanvasComponents(1,gObj));
	}

    IEnumerator FadeCanvasComponents(int inOrOut, GameObject gObj)
    {
        int numOfChild = gObj.transform.childCount;
        GameObject correntChild;

        if (inOrOut == 1) {
            yield return new WaitForSeconds(0.5f);
        }

        float deltaTime;
        deltaTime = (0.8f / numOfChild);

        for (int j = 1; j <= 8; j++)
        {
            for (int i = 0; i < numOfChild; i++)
            {
                correntChild = gObj.transform.GetChild(i).gameObject;
                correntChild.GetComponent<CanvasGroup>().alpha = correntChild.GetComponent<CanvasGroup>().alpha + inOrOut * 1 / 8f;
            }
            yield return new WaitForSeconds(0.07f);
        }

		if (inOrOut == -1) 
			gObj.SetActive (false);

//		_currentUI = GameOverCanvas.transform.Find("FinalScoreText").gameObject;
//		for (int i = 1; i <= 8; i++)
//		{
//			_currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 8f;
//			yield return new WaitForSeconds(0.07f);
//		}
//
//		_currentUI = GameOverCanvas.transform.Find("WaveNumberText").gameObject;
//		for (int i = 1; i <= 8; i++)
//		{
//			_currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 8f;
//			yield return new WaitForSeconds(0.07f);
//		}
//
//		_currentUI = GameOverCanvas.transform.Find("Score").gameObject;
//		for (int i = 1; i <= 4; i++)
//		{
//			_currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 4f;
//			yield return new WaitForSeconds(0.05f);
//		}
//
//		_currentUI = GameOverCanvas.transform.Find("Waves").gameObject;
//		for (int i = 1; i <= 4; i++)
//		{
//			_currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 4f;
//			yield return new WaitForSeconds(0.05f);
//		}
//
//		for (int i = 1; i <= 5; i++)
//		{
//			GameOverCanvas.transform.Find("PlayAgain").gameObject.GetComponent<CanvasGroup>().alpha = GameOverCanvas.transform.Find("PlayAgain").gameObject.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 5f;
//			GameOverCanvas.transform.Find("MainMenu").gameObject.GetComponent<CanvasGroup>().alpha = GameOverCanvas.transform.Find("MainMenu").gameObject.GetComponent<CanvasGroup>().alpha + inOrOut*1 / 5f;
//			yield return new WaitForSeconds(0.07f);
//		}

	}

    private void Start()
    {
        leaderBoardControllerScript = GameObject.Find("LeaderboardController").GetComponent<LeaderBoardControllerScript>();
    }

}
