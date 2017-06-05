using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn : MonoBehaviour {

	public GameObject grass00;
	public GameObject grass01;
	public GameObject grass02;
	public GameObject grass03;
	public GameObject grass04;

	public float chance00;
	public float chance01;
	public float chance02;
	public float chance03;
	public float chance04;

	private float sum;
	private float val;
	private float rot;


	void Start () {
		Ajust ();
		Select ();
	}

	void Ajust(){
		sum = chance00 + chance01 + chance02 + chance03 + chance04;
		val = Random.value*sum;
		rot = Mathf.Round (Random.value * 4f);
	}

	void Select(){
		float compVal = 0;

		compVal += chance00;
		if (val <= chance00) {
			Spawn (grass00, -90f, 0);
			return;
		}
		compVal += chance01;
		if (val <= compVal){
			Spawn (grass01, 0, -0.05f);
			return;
		}
		compVal += chance02;
		if (val <= compVal){
			Spawn (grass02, 0, 0);
			return;
		}
		compVal += chance03;
		if (val <= compVal){
			Spawn (grass03, 0, 0);
			return;
		}
		compVal += chance04;
		if (val <= compVal){
			Spawn (grass04, 0, 0);
			return;
		}
	}

	void Spawn(GameObject obj, float rot1, float yCorrection){
		GameObject inst = (GameObject)Instantiate (obj, transform.position, transform.rotation);
		Vector3 pos = transform.position;
		inst.transform.rotation = Quaternion.Euler (rot1, 90f * rot, 0);
		inst.transform.position = new Vector3(pos.x, yCorrection, pos.z);
		Destroy (gameObject);
	}
}
