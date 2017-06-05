using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCounter : MonoBehaviour {

	public float InitialSouls = 200f;
	public Text soulsText;

	private float souls;

	public void SetSouls(float value){
		souls = value;
	}

	public float GetSouls(){
		return souls;
	}

	private void Start () {
		SetSouls (InitialSouls);
	}

	private void Update(){
		UpdateUI ();
	}

	private void UpdateUI(){
		soulsText.text = Mathf.Round(GetSouls()).ToString();
	}
}