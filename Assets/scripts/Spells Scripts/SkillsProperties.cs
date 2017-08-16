using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsProperties : MonoBehaviour {

	[Header("To assign")]
	[SerializeField] private float damage;
	[SerializeField] private float cooldown;
	[SerializeField] private float range;
	[SerializeField] private GameObject sideEffect;

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
		return sideEffect;
	}

	public GameObject GetInvoker () {
		return invoker;
	}

	public void SetInvoker (GameObject _invoker) {
		invoker = _invoker;
	}

	public void SetDamage (float _damage) {
		damage = _damage;
	}

	public void SetCooldown (float _cooldown) {
		cooldown = _cooldown;
	}

	public void SetRange (float _range) {
		range = _range;
	}

	public void SetEffect (GameObject _sideEffect) {
		sideEffect = _sideEffect;
	}
}
