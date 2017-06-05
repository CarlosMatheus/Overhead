using UnityEngine;

public class Node : MonoBehaviour {

	public Color hoverColor;

	private Renderer rend;
	private Color startColor;
	private GameObject tower;

	void Start(){
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}
		
	void OnMouseEnter (){
		rend.material.color = hoverColor;
	}

	void OnMouseExit (){
		rend.material.color = startColor;
	}

	void OnMouseDown(){
		if (tower != null) {
			Debug.Log ("can't build there! - TODO: Display on screen.");
			return;
		}
		GameObject towerToBuild = BuildManager.instance.GetTowerToBuild ();
		tower = (GameObject)Instantiate (towerToBuild, transform.position, transform.rotation);
	}
}
