using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn : MonoBehaviour {

	public GameObject[] arr;
	public float[] chance;

	private float sum;
	private float val;
	private float rot;

	void Start () {
		Ajust ();
		Select ();
	}

	//Ajust correctly the chances and randoms rotations values
	void Ajust(){
		sum = 0;
		for (int i = 0; i < chance.Length; i++)
			sum += chance [i];
		val = Random.value*sum;
		rot = Mathf.Round (Random.value * 4f);
	}

	//select randomly a element of the array
	void Select(){
		float compVal = 0;
		for (int i = 0; i < arr.Length; i++) {
			compVal += chance[i];
			if (val <= compVal) {
				Spawn (i);
				return;
			}
		}
	}

	//spawn in a random y rotation, the quaternion need to be converted to vector 3 to oparate it
	void Spawn(int i){
		Quaternion rotat = arr [i].transform.rotation;
		Vector3 rotatInVector = rotat.eulerAngles;
		rotatInVector = new Vector3 (rotatInVector.x, 90 * rot, rotatInVector.z);
		Instantiate (arr [i], transform.position, Quaternion.Euler(rotatInVector));
		Destroy (gameObject);
	}
}
