using UnityEngine;

public class Node : MonoBehaviour {

	public Material hoverMaterial;

	private Renderer rend;
	private Material originalMaterial;
	private GameObject tower;

	void Start(){
		// Getting reference of Renderer of this.gameObject
		rend = GetComponent<Renderer> ();

		// Setting up material properties
		originalMaterial = rend.material;                // originalMaterial has always to know what's the initial state
		hoverMaterial.color = originalMaterial.color;    // hoverMaterial just carries emission info, color is from originalMaterial
	}
		
	void OnMouseEnter (){
		rend.material = hoverMaterial;
	}

	void OnMouseExit (){
		rend.material = originalMaterial;
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
