using UnityEngine;
using System.Collections;

public class demonStateChasing : StateMachineBehaviour {

	private DemonBehavior db;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		db = animator.transform.root.GetComponent<DemonBehavior>();
		DemonBehavior.mouthtarget = 1f;
		animator.SetFloat("Movement", 1f);
		animator.speed = 2;

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to
	
		db.aso.volume = Mathf.Lerp(db.aso.volume, 1, Time.deltaTime);
		db.RotateTowards(db.player);
		db.agent.SetDestination(db.player.position);
		
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.
		DemonBehavior.mouthtarget = 0f;
		animator.SetFloat("Movement", .25f);
		animator.speed = 1;

	}

}
