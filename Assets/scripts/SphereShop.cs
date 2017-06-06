using UnityEngine;
using UnityEngine.EventSystems;

public class SphereShop : MonoBehaviour {

	public float initialIntensity = 0.5f;
	public float hoverIntensity = 1f;
	public Transform player;
	public Transform center;
	public GameObject shop;

	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private BuildManager buildManager;
	private Light light;

	private void Start(){
		soulsCounter = SoulsCounter.instance;
		scoreCounter = ScoreCounter.instance;
		buildManager = BuildManager.instance;
		light = GetComponent<Light> ();
		light.intensity = initialIntensity;
		shop.SetActive (false);
	}

	private void Update () {
		if (IsInMainTower ()) {
			if (!soulsCounter.CanBuild ())
				CantBuild ();
			if (Input.GetMouseButtonDown (1))
				DesactiveShop ();
		} else {
			CantBuild ();
			DesactiveShop ();
		}
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

	private void CantBuild(){
		buildManager.SetTowerToBuild (null);
		buildManager.DestroyTranspTowerInst ();
	}

	private void DesactiveShop(){
		buildManager.SetTowerToBuild (null);
		buildManager.DestroyTranspTowerInst ();
		shop.SetActive (false);
	}

	private bool IsInMainTower(){
		if (Vector3.Distance (player.position, center.position) <= 1f) {
			return true;
		} else
			return false;
	}
}
