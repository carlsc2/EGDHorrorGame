using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class demonStateSwallow : StateMachineBehaviour {

	private Image blackscreen;
	private UnityStandardAssets.Characters.FirstPerson.MouseLook ml;
	private Transform pcamera;
	private Transform player;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		GameObject stomach = GameObject.FindGameObjectWithTag("Finish");
		pcamera = player.GetComponentInChildren<Camera>().transform;

		player.position = stomach.transform.position;
		blackscreen = GameObject.FindGameObjectWithTag("blackscreen").GetComponent<Image>();

		ml = new UnityStandardAssets.Characters.FirstPerson.MouseLook();
		ml.Init(player, pcamera);

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		blackscreen.color = Color.Lerp(blackscreen.color, Color.black, Time.deltaTime);
		if(blackscreen.color.a >= .98f) {
			SceneManager.LoadScene("youlose");
		}
		ml.LookRotation(player, pcamera);

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
