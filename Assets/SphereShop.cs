using UnityEngine;
using UnityEngine.EventSystems;

public class SphereShop : MonoBehaviour {

	public float initialIntensity = 0.5f;
	public float hoverIntensity = 1f;
	public GameObject shop;

	private BuildManager buildManager;
	private Light light;

	void Start(){
		buildManager = BuildManager.instance;
		light = GetComponent<Light> ();
		light.intensity = initialIntensity;
		shop.SetActive (false);
	}

	void Update () {
		
	}

	void OnMouseEnter (){
		//Avoid pointing to something with a UI element in front of it
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		light.intensity = hoverIntensity;
		Debug.Log ("entrou");
	}

	void OnMouseExit (){
		light.intensity = initialIntensity;
		Debug.Log ("saiu");
	}

	void OnMouseDown(){
		//Avoid pointing to something with a UI element in front of it
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		//if the towerToBuild variable is null dont do anything 
		if (buildManager.GetTowerToBuild () == null) {
			return;
		}
		ActiveShop ();
	}

	void ActiveShop(){
		shop.SetActive (true);
	}

}
