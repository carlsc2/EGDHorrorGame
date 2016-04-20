using UnityEngine;
using System.Collections;

public class DemonBehavior : MonoBehaviour {

	[HideInInspector]
	public NavMeshAgent agent;
	[HideInInspector]
	public AudioSource aso;
	[HideInInspector]
	public Transform player;

	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController pcontrol;
	
	public AudioSource screamsound;

	public static float mouthtarget = 0f;

	//current levels
	public float fieldOfViewAngle = 20;
	public float sightDistance = 15;
	public float wanderDistance = 30;
	public float searchTime = 15; //how long to search before giving up

	[HideInInspector]
	public float lastSightingTime = -1000;//time of last player sighting
	[HideInInspector]
	public Vector3 lastSightingPosition;//position of last sighting

	public SkinnedMeshRenderer eyemesh;


	//base levels
	private float _sightDistance;
	private float _fieldOfViewAngle;

	public bool playerInSight = false;//player is in sight to be chased
	public bool playerInRange = false;//player is in range to be searched for
	public bool is_searching = false;//searching for player?

	public float screamDelay = 10f;//minimum time between screams
	private float lastScreamTime = -1000;

	private Material eyeMat;

	public Transform sightpoint;

	public Animator statemachine;



	void OnDrawGizmosSelected() {
		if (playerInSight) {
			Gizmos.color = new Color(1, 1, 0, .25f);
		}
		else {
			Gizmos.color = new Color(1, 0, 0, .25f);
		}
		
		Gizmos.DrawSphere(transform.position, sightDistance);
		Gizmos.DrawSphere(transform.position, wanderDistance);

		


		float rayRange = 10.0f;
		float halfFOV = fieldOfViewAngle / 2.0f;
		Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
		Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
		Vector3 leftRayDirection = leftRayRotation * transform.forward;
		Vector3 rightRayDirection = rightRayRotation * transform.forward;
		Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
		Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);

		Gizmos.color = Color.blue;
		if (agent) Gizmos.DrawLine(sightpoint.position, agent.destination);
		if(agent) Gizmos.DrawCube(agent.destination, Vector3.one);
	}

	// Use this for initialization
	void Start () {
		_sightDistance = sightDistance;
		_fieldOfViewAngle = fieldOfViewAngle;

		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		pcontrol = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

		aso = GetComponent<AudioSource>();
		eyeMat = eyemesh.material;
		//StartCoroutine(do_update());
	}

	bool check_line_of_sight() {
		//check if player is in line of sight
		Vector3 direction = player.position - sightpoint.position;
		float dist = direction.magnitude;
		float angle = Vector3.Angle(direction, sightpoint.forward);
		bool tmp = playerInSight;
		playerInSight = false;
		if (dist < sightDistance) {
			playerInRange = true;
			// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;
				// ... and if a raycast towards the player hits something...
				if (Physics.Raycast(sightpoint.position, direction.normalized, out hit, sightDistance)) {
					// ... and if the raycast hits the player...
					if (hit.collider.transform.root == player) {
						// ... the player is in sight.
						if (!tmp && (Time.time - lastScreamTime > screamDelay)) {
							screamsound.Play();
							lastScreamTime = Time.time;
						}
						playerInSight = true;						
						lastSightingTime = Time.time;
						lastSightingPosition = player.position;
					}
				}
			}
		}
		else {
			playerInRange = false;
		}
		return true;
	}

	void adapt_alert_radius() {
		//sneaky beaky adaptive fov
		if (pcontrol.is_walking() || !playerInSight) {
			fieldOfViewAngle = _fieldOfViewAngle;
		}
		else {
			fieldOfViewAngle = _fieldOfViewAngle * 3;
		}

		//fear based sight distance increase
		float fear_rate = HBListener.Instance.avgPulse / (float)HBListener.Instance.base_rate;
		sightDistance = _sightDistance * fear_rate;
	}


	public bool is_navigating() {
		// Check if we've reached the destination
		if (!agent.pathPending) {
			if (agent.remainingDistance <= agent.stoppingDistance) {
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
					return false;
				}
			}
		}
		return true;
	}

	public void RotateTowards(Transform target) {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
	}

	/*float PathLength(NavMeshPath path) {
		if (path.corners.Length < 2)
			return 0;

		Vector3 previousCorner = path.corners[0];
		float lengthSoFar = 0.0F;
		int i = 1;
		while (i < path.corners.Length) {
			Vector3 currentCorner = path.corners[i];
			lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
			previousCorner = currentCorner;
			i++;
		}
		return lengthSoFar;
	}*/

	void Update() {
		adapt_alert_radius();
		check_line_of_sight();
		is_searching = Time.time - lastSightingTime < searchTime;

		statemachine.SetBool("player_in_sight", playerInSight);
		statemachine.SetBool("player_in_range", playerInRange);
		statemachine.SetBool("is_searching", is_searching);
		statemachine.SetFloat("MouthBlend", Mathf.MoveTowards(statemachine.GetFloat("MouthBlend"), mouthtarget, Time.deltaTime * 10f));

		Color eyecolor = eyeMat.GetColor("_EmissionColor");
		Color targetcolor = playerInSight ? Color.red : Color.white;
		int brightval = playerInRange ? 1 : 0;
		eyeMat.SetColor("_EmissionColor", Color.Lerp(eyecolor, targetcolor * brightval, Time.deltaTime));
	}
}
