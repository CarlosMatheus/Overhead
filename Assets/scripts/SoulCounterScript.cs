using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCounterScript : MonoBehaviour {

    //Holders
    CanvasGroup canvasGroup;
    private float _currentValue;
    private Vector3 finalPos;
    private Vector3 finalScale;

    //Attributes
    public float speed = 2f;
    public float deltaY = 3f;
    public float deltaScale = 0.7f;
    public float destroyTime = 3f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        finalPos = transform.position + new Vector3(0, deltaY, 0);
        finalScale = deltaScale * transform.localScale;

        //Auto-destruct itself
        Destroy(gameObject, destroyTime);

        //Fade
        StartCoroutine(FadeText());
    }
    void Update()
    {
        //Billboard Effect
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        //Go up
        transform.position = transform.position + speed * (finalPos - transform.position) * Time.deltaTime;

        //Scaling!
        transform.localScale = transform.localScale + speed * (finalScale - transform.localScale) * Time.deltaTime;
    }

    IEnumerator FadeText()
    {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0.01)
        {
            _currentValue = canvasGroup.alpha - 0.6f * Time.deltaTime;
            canvasGroup.alpha = _currentValue;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
