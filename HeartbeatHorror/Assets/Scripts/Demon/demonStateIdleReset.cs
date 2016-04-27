using UnityEngine;
using System.Collections;

public class demonStateIdleReset : StateMachineBehaviour {

	//blend values for idle states
	static float[] statevals = new float[] { 0, 0.25f, 0.5f, 0.75f, 1f };

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the first frame of the state being played
		animator.SetFloat("idleAction", statevals[Random.Range(0, statevals.Length)]);
		animator.SetLayerWeight(3, 0);

		if(demonStateIdle.idletimeout == -1) {
			demonStateIdle.idletimeout = Time.time + Random.Range(5, 10);
		}
		else {
			float fearscale = HBListener.Instance.avgPulse / (float)HBListener.Instance.base_rate;

			if (fearscale < 1.05f) {//if under 5% scared, phase out
				animator.SetTrigger("phaseout");
			}else if (Time.time > demonStateIdle.idletimeout) {
				demonStateIdle.idletimeout = -1;
				animator.SetBool("is_active", true);
			}
		}


	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

}
