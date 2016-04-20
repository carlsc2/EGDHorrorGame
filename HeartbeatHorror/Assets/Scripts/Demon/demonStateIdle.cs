using UnityEngine;
using System.Collections;

public class demonStateIdle : StateMachineBehaviour {

	//blend values for idle states
	static float[] statevals = new float[] { 0, 0.25f, 0.5f, 0.75f, 1f };

	static float idletimeout = -1;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		animator.SetFloat("idleAction", statevals[Random.Range(0, statevals.Length)]);

		if(idletimeout == -1) {
			idletimeout = Time.time + Random.Range(5, 10);
		}
		else {
			if (Time.time > idletimeout) {
				idletimeout = -1;
				animator.SetFloat("Movement", .5f);
				animator.SetBool("is_active", true);
			}
		}


	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.

	}

}
