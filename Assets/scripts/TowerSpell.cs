using UnityEngine;

public class TowerSpell : MonoBehaviour {

	public Transform target;
	public GameObject impactEffect;
	public float speed = 15f;

	// This puclic function is accessed by the caster and it passes the target
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

	// Follow the target untill hit it
	void Follow() {
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		//this is to avoid the speel to overpass the target
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}
		transform.LookAt (target.position);
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	// Will hit the target, intantiate the impactEffect, make damage, then will destroy the spell and its effect
	void HitTarget() {
		target.GetComponent<TargetSelection> ().TakeDamageBy(this.gameObject);

		GameObject invoker = GetComponent<SkillsProperties> ().GetInvoker ();
		PropertiesManager pm = invoker.GetComponent<PropertiesManager>();
		if (pm != null) {
			if (pm.HasSoulBonusEffect ()) {
				if (Random.Range (0f, 1f) < pm.GetSoulBonusChance ()) {
					SoulsCounter.instance.AddSouls (invoker.tag);
					Debug.Log ("Soul de bonus!");
				}
			}
		}

		if (impactEffect != null) {
			GameObject effect = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
			Destroy (effect, 2f);
		}

		if (GetComponent<SkillsProperties> ().GetEffect() != null) {  // If this spell has a effect

			// Instatiate it
			GameObject sideEffect = (GameObject)Instantiate (
				GetComponent<SkillsProperties> ().GetEffect (), 
				target.gameObject.transform.position, 
				target.gameObject.transform.rotation
			);

			// Set sideEffect target
			sideEffect.GetComponent<SideEffect> ().SetTarget (target.gameObject);

			// Set sideEffect invoker
			sideEffect.GetComponent<SkillsProperties> ().SetInvoker (GetComponent<SkillsProperties>().GetInvoker ());

			// Att sideEffect values
			sideEffect.GetComponent<SideEffect>().SetBurnRate (GetComponent<SkillsProperties> ().GetBurnRate ());
			sideEffect.GetComponent<SideEffect> ().SetSlowFactor (GetComponent<SkillsProperties> ().GetSlowFactor ());

			// Start sideEffect effects
			sideEffect.GetComponent<SideEffect> ().StartEffect ();
		}
		Destroy (gameObject);
	}
}
