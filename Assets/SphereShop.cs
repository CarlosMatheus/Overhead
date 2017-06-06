using UnityEngine;
using UnityEngine.EventSystems;

public class SphereShop : MonoBehaviour {

	public float initialIntensity = 0.5f;
	public float hoverIntensity = 1f;
	public GameObject shop;

	private BuildManager buildManager;
	private Light light;

	private void Start(){
		buildManager = BuildManager.instance;
		light = GetComponent<Light> ();
		light.intensity = initialIntensity;
		shop.SetActive (false);
	}

	private void Update () {
		if (Input.GetMouseButtonDown (1))
			DesactiveShop ();
	}

	private void OnMouseEnter (){
		//Avoid pointing to something with a UI element in front of it
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		light.intensity = hoverIntensity;
	}

	private void OnMouseExit (){
		light.intensity = initialIntensity;
	}

	private void OnMouseDown(){
		//Avoid pointing to something with a UI element in front of it
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		ActiveShop ();
	}

	private void ActiveShop(){
		shop.SetActive (true);
	}

	private void DesactiveShop(){
		buildManager.SetTowerToBuild (null);
		buildManager.DestroyTranspTowerInst ();
		shop.SetActive (false);
	}
}
