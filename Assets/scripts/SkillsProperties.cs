using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsProperties : MonoBehaviour {

	[Header("To assign")]
	public float damage;
	public float cooldown;
	public float range;

	[Header("Auto assign")]
	public GameObject invoker;

}
