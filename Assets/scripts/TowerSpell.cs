using UnityEngine;
using System.Collections;

public class TowerSpell : MonoBehaviour {

	public Transform target;
	[SerializeField] private GameObject impactEffect = null;
	private float speed = 15f;

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

        // Impact animation effect
        if (impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
        }

        // Soul bonus effect
        GameObject invoker = GetComponent<SkillsProperties> ().GetInvoker ();
        if (invoker != null)
        {
            PropertiesManager pm = invoker.GetComponent<PropertiesManager>();
            if (pm != null)
            {
                if (pm.HasSoulBonusEffect())
                {
                    if (Random.Range(0f, 1f) < pm.GetSoulBonusChance())
                    {
                        SoulsCounter.instance.AddSouls(invoker.tag);
                        Debug.Log("Soul de bonus!");
                    }
                }

                if (pm.HasSlowTimeEffect())
                {
                    if (Random.Range(0f, 1f) < pm.GetSlowTimeChance())
                    {
                        StartCoroutine(StopThisEnemy(target, pm.GetEffectDuration()));
                        Debug.Log("Freeze!");
                    }
                }

                if (pm.HasFatalHitEffect())
                {
                    if (Random.Range(0f, 1f) < pm.GetFatalHitChance())
                    {
                        Destroy(target);
                        Debug.Log("Hit kill!");
                        return;
                    }
                }
            }
            else
                Debug.LogError("We have null exception on propertiesManager in " + gameObject.name);
        }
        else
            Debug.LogError("We have null exception on invoker in " + gameObject.name);

        // Side effect
		if (GetComponent<SkillsProperties> ().GetEffect() != null) {  // If this spell has a effect
            
			// Instatiate it
			GameObject sideEffect = Instantiate (
				GetComponent<SkillsProperties> ().GetEffect (), 
				target.position, 
				target.rotation
			);

            // Set sideEffect target
            sideEffect.GetComponent<SideEffect> ().SetTarget (target.gameObject);

			// Set sideEffect invoker
			sideEffect.GetComponent<SkillsProperties> ().SetInvoker (GetComponent<SkillsProperties>().GetInvoker ());

			// Att sideEffect values with SkillProperties values from spell hierity from tower
			sideEffect.GetComponent<SideEffect>().SetBurnRate (GetComponent<SkillsProperties> ().GetBurnRate ());
			sideEffect.GetComponent<SideEffect> ().SetSlowFactor (GetComponent<SkillsProperties> ().GetSlowFactor ());
			sideEffect.GetComponent<SideEffect> ().SetRangeRadius (GetComponent<SkillsProperties> ().GetRangeRadius ());
			sideEffect.GetComponent<SideEffect> ().SetEffectDuration (GetComponent<SkillsProperties> ().GetEffectDuration ());

			// Start sideEffect effects
			sideEffect.GetComponent<SideEffect> ().StartEffect ();
		}

        // Finalization
		Destroy (gameObject);
	}

    IEnumerator StopThisEnemy(Transform _enemy, float _time)
    {
        Enemy enemy = _enemy.gameObject.GetComponent<Enemy>();
        enemy.SetSpeed(0f);
        yield return new WaitForSeconds(_time);
        enemy.ReturnToOriginalSpeed();
    }
}
