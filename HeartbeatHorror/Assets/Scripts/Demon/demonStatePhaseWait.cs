using UnityEngine;
using System.Collections;

public class demonStatePhaseWait : StateMachineBehaviour {

	private DemonBehavior db;
	private bool phased;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		db = animator.transform.root.GetComponent<DemonBehavior>();
		//ensure demon is out of the world
		db.phase_out_of_world();
		phased = false;

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!phased) {
			float fearscale = HBListener.Instance.avgPulse / (float)HBListener.Instance.base_rate;
			if (fearscale > 1.1f) {//if over 10% scared, phase in
				Vector3 spawnpoint = Random.insideUnitSphere * Random.Range(db.wanderDistance,db.wanderDistance*2) + db.player.position;//random point near-ish to player but not too close
				NavMeshHit hit;
				Debug.DrawLine(db.transform.position, spawnpoint, Color.yellow);
				if (NavMesh.SamplePosition(spawnpoint, out hit, 30, 1)) {
					//success
					db.agent.Warp(hit.position);
					//db.agent.enabled = false;
					//db.transform.position = hit.position;
					//db.agent.enabled = true;
					animator.SetTrigger("phasein");
					phased = true;
				}
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		db.phase_into_world();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
