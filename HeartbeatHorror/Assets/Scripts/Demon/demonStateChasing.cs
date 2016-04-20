using UnityEngine;
using System.Collections;

public class demonStateChasing : StateMachineBehaviour {

	private DemonBehavior db;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		db = animator.transform.root.GetComponent<DemonBehavior>();
		DemonBehavior.mouthtarget = 1f;

		float fearscale = (HBListener.Instance.avgPulse - HBListener.Instance.base_rate) / (0.17f * HBListener.Instance.base_rate);
		animator.speed = Mathf.Lerp(2,5,fearscale);

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to
	
		db.aso.volume = Mathf.Lerp(db.aso.volume, 1, Time.deltaTime);
		db.RotateTowards(db.player);
		db.agent.SetDestination(db.player.position);

		float fearscale = (HBListener.Instance.avgPulse - HBListener.Instance.base_rate) / (0.17f * HBListener.Instance.base_rate);
		animator.speed = Mathf.Lerp(2, 5, fearscale);

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.
		DemonBehavior.mouthtarget = 0f;
		animator.speed = 1;

	}

}
