﻿using UnityEngine;
using System.Collections;

public class demonStateWandering : StateMachineBehaviour {

	private DemonBehavior db;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		db = animator.transform.root.GetComponent<DemonBehavior>();

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to

		if (!db.is_navigating()) {
			Vector3 wanderpoint = (Random.insideUnitSphere * db.wanderDistance) + db.transform.position;
			db.agent.SetDestination(wanderpoint);
		}

		if (db.aso.volume > 0) {
			db.aso.volume = Mathf.Lerp(db.aso.volume, 0, Time.deltaTime);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.

	}

}