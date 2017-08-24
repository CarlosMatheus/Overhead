using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesManager : MonoBehaviour {

	// Atributes from spell
	private float damage;
	private float cooldown;
	private float range;
	private float soulBonusChance = 10f;
	private bool soulBonusActivation = false;
	private float burnRate;
	private float slowFactor;

	public float GetDamage () {
		return damage;
	}

	public float GetCooldown () {
		return cooldown;
	}

	public float GetRange () {
		return range;
	}

	public float GetSlowFactor () {
		return slowFactor;
	}

	public float GetBurnRate() {
		return burnRate;
	}

	public void SetDamage (float multiplicationFactor) {
		damage *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetDamage ();
	}

	public void SetCooldown (float multiplicationFactor) {
		cooldown *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
	}

	public void SetRange (float multiplicationFactor) {
		range *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetRange ();
	}

	public void SetBurnRate (float multiplicationFactor) {
		burnRate *= multiplicationFactor;
	}

	public void SetSlowFactor (float multiplicationFactor) {
		slowFactor *= multiplicationFactor;
	}

	public bool HasSoulBonusEffect () {
		return soulBonusActivation;
	}

	public void SetSoulBonusEffect (bool _act) {
		soulBonusActivation = _act;
	}

	public float GetSoulBonusChance () {
		return soulBonusChance/100;
	}

	public void SetSoulBonusChance (float multiplicationFactor) {
		soulBonusChance *= multiplicationFactor;
	}

	public void SetValues (SkillsProperties sp) {

		damage = sp.GetDamage ();
		cooldown = sp.GetCooldown ();
		range = sp.GetRange ();
		burnRate = sp.GetBurnRate ();
		slowFactor = sp.GetSlowFactor ();
	}
}
