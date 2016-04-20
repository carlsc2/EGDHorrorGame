using UnityEngine;
using System.Collections;

public class demonStateIdle : StateMachineBehaviour {

	//blend values for idle states
	static float[] statevals = new float[] { 0, 0.25f, 0.5f, 0.75f, 1f };

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		animator.SetFloat("idleAction", statevals[Random.Range(0, statevals.Length)]);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.

	}

}
