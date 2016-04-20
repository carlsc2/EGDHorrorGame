using UnityEngine;
using System.Collections;

public class demonStateSearching : StateMachineBehaviour {

	private DemonBehavior db;
	private float searchDistance;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//called on the first frame of the state being played
		db = animator.transform.root.GetComponent<DemonBehavior>();
		searchDistance = 1 + db.agent.stoppingDistance;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to

		if (!db.is_navigating()) {
			Vector3 tpos = db.is_searching ? db.lastSightingPosition : db.transform.position;

			/* TODO: weight wanderpoint towards player based on fear level */

			float fear_weight = (HBListener.Instance.avgPulse / (float)HBListener.Instance.base_rate - 1) / 0.2f;

			Debug.Log(fear_weight);

			Vector3 playerpoint = db.player.position;
			Vector3 wanderpoint = (Vector3)(Random.insideUnitCircle * searchDistance) + tpos;

			wanderpoint = Vector3.Lerp(wanderpoint, playerpoint, fear_weight);

			NavMeshHit nhit;
			if (NavMesh.SamplePosition(wanderpoint, out nhit, searchDistance, 1)) {
				Debug.DrawLine(db.transform.position, wanderpoint, Color.cyan);
				NavMeshPath path = new NavMeshPath();
				if (!db.agent.CalculatePath(nhit.position, path)) {
					return;
				}
				//only navigate to seeable areas
				RaycastHit hit;
				Vector3 direction = nhit.position - db.sightpoint.position;
				if (!Physics.Raycast(db.sightpoint.position, direction.normalized, out hit, direction.magnitude * .95f)) {
					db.agent.SetPath(path);
				}
			}
			searchDistance += 0.5f;			
		}
		db.aso.volume = Mathf.Lerp(db.aso.volume, .25f, Time.deltaTime);

		//increase search speed based on player fear
		animator.speed = HBListener.Instance.avgPulse / HBListener.Instance.base_rate;
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//called on the last frame of a transition to another state.
		animator.speed = 1;

	}
}
