using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderScript : MonoBehaviour {

    private GameObject fadeCanvas;
    private float _currentValue;

    void Start () {
        fadeCanvas = this.transform.Find("FadeCanvas").gameObject;
        StartCoroutine(FadeToWhite());
    }

    IEnumerator FadeToWhite()
    {
        fadeCanvas.SetActive(true);
        fadeCanvas.GetComponent<CanvasGroup>().alpha = 1;
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha > 0.01)
        {
            _currentValue = fadeCanvas.GetComponent<CanvasGroup>().alpha - 0.9f * Time.deltaTime;
            fadeCanvas.GetComponent<CanvasGroup>().alpha = _currentValue;
            yield return null;
        }
        fadeCanvas.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(false);
    }
}
