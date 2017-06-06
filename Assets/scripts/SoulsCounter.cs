using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCounter : MonoBehaviour {

	//Make it easier to instantiate:
	public static SoulsCounter instance;
	public float InitialSouls = 200f;
	public Text soulsText;

	private float souls;
	private float towerPrice;

	public void SetSouls(float value){
		souls = value;
	}

	public float GetSouls(){
		return souls;
	}

	public void SetTowerPrice(float value){
		towerPrice = value;
	}

	public float GetTowerPrice(){
		return towerPrice;
	}

	public void BuildTower(){
		souls -= towerPrice;
	}

	public bool CanBuild(){
		if (towerPrice <= souls)
			return true;
		else
			return false;
	}

	private void Start () {
		SetSouls (InitialSouls);
		towerPrice = 10f;
		instance = this;
	}

	private void Update(){
		UpdateUI ();
	}

	private void UpdateUI(){
		soulsText.text = Mathf.Round(GetSouls()).ToString();
	}
}