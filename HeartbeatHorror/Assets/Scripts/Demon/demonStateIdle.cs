﻿using UnityEngine;
using System.Collections;

public class demonStateIdle : StateMachineBehaviour {

	private DemonBehavior db;

	//blend values for idle states
	static float[] statevals = new float[] { 0, 0.25f, 0.5f, 0.75f, 1f };

	public static float idletimeout = -1;

	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
		db = animator.transform.root.GetComponent<DemonBehavior>();
	}

	override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
		idletimeout = -1;
		animator.SetLayerWeight(3, 1);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to
		if(db == null) {
			db = animator.transform.root.GetComponent<DemonBehavior>();
		}

		if (db.playerInRange) {
			animator.SetBool("is_active", true);
		}

	}
}