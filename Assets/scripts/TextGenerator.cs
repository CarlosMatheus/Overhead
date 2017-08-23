using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour {

    public string[] phrases;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(GenerateText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GenerateText()
    {
        text.text = phrases[0];

        yield return new WaitForSeconds(1f);

        for (int i = 1; i < phrases.Length; i++)
        {
            text.text = phrases[i];
            yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));
        }
    }
}
