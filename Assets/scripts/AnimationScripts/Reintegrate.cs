using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reintegrate : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool ("start", false);
		animator.gameObject.GetComponent<PlayerController> ().teleporting = true;
	}

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.gameObject.GetComponent<PlayerController> ().teleporting = false;
	}
}
