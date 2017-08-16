using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsProperties : MonoBehaviour {

	[Header("To assign")]
	[SerializeField] private float damage;
	[SerializeField] private float cooldown;
	[SerializeField] private float range;
	[SerializeField] private GameObject effect;

	private GameObject invoker;

	public float GetDamage () {
		return damage;
	}

	public float GetCooldown () {
		return cooldown;
	}

	public float GetRange () {
		return range;
	}

	public GameObject GetEffect () {
		return effect;
	}

	public GameObject GetInvoker () {
		return invoker;
	}

	public void SetInvoker (GameObject _invoker) {
		invoker = _invoker;
	}

}
