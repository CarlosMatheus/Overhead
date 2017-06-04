using UnityEngine;

public class TowerSpell : MonoBehaviour {

	public Transform target;
	public GameObject impactEffect;
	public float speed = 15f;

	//this puclic function is accessed by the caster and it passes the target
	public void Seek (Transform _target){
		target = _target;
	}

	void Update () {
		if (target == null) {
			Destroy (gameObject);
			return;
		}
		Follow ();
	}

	//Fallow the target untill hit it
	void Follow(){
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		//this is to avoid the speel to overpass the target
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	//Will hit the target, intantiate the impactEffect, make damage, then will destroy the spell and its effect
	void HitTarget(){
		target.GetComponent<TargetSelection> ().HP -= GetComponent<SkillsProperties> ().damage;

		GameObject effect = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (effect, 2f);
		Destroy (gameObject);
	}
}
