using UnityEngine;
using System.Collections;

public class demonStateChasing : StateMachineBehaviour {

	private DemonBehavior db;
	private Transform player;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		db = animator.transform.root.GetComponent<DemonBehavior>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		

		float fearscale = (HBListener.Instance.avgPulse - HBListener.Instance.base_rate) / (0.17f * HBListener.Instance.base_rate);
		//animator.speed = Mathf.Lerp(2,5,fearscale);
		DemonBehavior.target_speed = Mathf.Lerp(6, 12, fearscale);

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to

		if (eatPlayer.killed) {
			return;
		}

		//distance based mouth open
		//max open at 3, 10% open at 10

		DemonBehavior.mouthtarget = 1 - ((Vector3.Distance(db.transform.position,player.position) - 3) / 8f);

		db.aso.volume = Mathf.Lerp(db.aso.volume, 1, Time.deltaTime);
		db.RotateTowards(db.player);
		db.agent.SetDestination(db.player.position);

		float fearscale = (HBListener.Instance.avgPulse - HBListener.Instance.base_rate) / (0.17f * HBListener.Instance.base_rate);
		//animator.speed = Mathf.Lerp(2, 5, fearscale);
		DemonBehavior.target_speed = Mathf.Lerp(6, 12, fearscale);

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.
		DemonBehavior.mouthtarget = 0f;
		//animator.speed = 1;
		DemonBehavior.target_speed = 1;

	}

}
