using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsProperties : MonoBehaviour {

	[Header("To assign")]
	[SerializeField] private float damage;
	[SerializeField] private float cooldown;
	[SerializeField] private float range;
	[SerializeField] private GameObject sideEffect;
	[SerializeField] private float burnRate;
	[SerializeField] private float slowFactor;
	[SerializeField] private float rangeRadius;
	[SerializeField] private float effectDuration;

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

	public float GetBurnRate () {
		return burnRate;
	}

	public float GetSlowFactor () {
		return slowFactor;
	}

	public float GetRangeRadius () {
		return rangeRadius;
	}

	public float GetEffectDuration () {
		return effectDuration;
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

	public void SetSideEffectValues (float _burnRate, float _slowFactor, float _range, float _duration) {
		burnRate = _burnRate;
		slowFactor = _slowFactor;
		rangeRadius = _range;
		effectDuration = _duration;
	}
}
