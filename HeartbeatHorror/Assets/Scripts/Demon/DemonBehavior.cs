using UnityEngine;
using System.Collections;

public class DemonBehavior : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform player;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController pcontrol;
	private AudioSource aso;

	public AudioClip screamsound;

	//current levels
	public float fieldOfViewAngle = 20;
	public float sightDistance = 15;
	public float searchDistance = 30;
	public float searchTime = 15; //how long to search before giving up

	private float lastSightingTime = -1000;//time of last player sighting
	private Vector3 lastSightingPosition;//position of last sighting


	//base levels
	private float _sightDistance;
	private float _fieldOfViewAngle;

	public bool playerInSight = false;//player is in sight to be chased
	public bool playerInRange = false;//player is in range to be searched for
	public bool is_searching = false;//searching for player?

	private Vector3 wanderpoint;

	public float screamDelay = 10f;//minimum time between screams
	private float lastScreamTime = -1000;

	public Material eyeMat;

	

	void OnDrawGizmosSelected() {
		if (playerInSight) {
			Gizmos.color = new Color(1, 1, 0, .25f);
		}
		else {
			Gizmos.color = new Color(1, 0, 0, .25f);
		}
		
		Gizmos.DrawSphere(transform.position, sightDistance);
		Gizmos.DrawSphere(transform.position, searchDistance);

		


		float rayRange = 10.0f;
		float halfFOV = fieldOfViewAngle / 2.0f;
		Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
		Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
		Vector3 leftRayDirection = leftRayRotation * transform.forward;
		Vector3 rightRayDirection = rightRayRotation * transform.forward;
		Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
		Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);

		Gizmos.color = Color.blue;
		if (agent) Gizmos.DrawLine(transform.position, agent.destination);
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
	}

	bool check_line_of_sight() {
		//check if player is in line of sight
		Vector3 direction = player.position - transform.position;
		float dist = direction.magnitude;
		float angle = Vector3.Angle(direction, transform.forward);
		bool tmp = playerInSight;
		playerInSight = false;
		if (dist < sightDistance) {
			playerInRange = true;
			// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;
				// ... and if a raycast towards the player hits something...
				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sightDistance)) {
					// ... and if the raycast hits the player...
					if (hit.collider.transform.root == player) {
						// ... the player is in sight.
						if (!tmp && (Time.time - lastScreamTime > screamDelay)) {
							aso.PlayOneShot(screamsound, 2);
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
		if (pcontrol.is_walking()) {
			fieldOfViewAngle = _fieldOfViewAngle;
		}
		else {
			fieldOfViewAngle = _fieldOfViewAngle * 3;
		}

		//fear based sight distance increase
		float fear_rate = HBListener.Instance.avgPulse / HBListener.Instance.base_rate;
		sightDistance = _sightDistance * fear_rate;
	}


	bool is_navigating() {
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

	void RotateTowards(Transform target) {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
	}


	// Update is called once per frame
	void Update () {
		adapt_alert_radius();
		check_line_of_sight();

		is_searching = Time.time - lastSightingTime < searchTime;



		if (playerInSight) {//if player in sight, chase the player
			aso.volume = Mathf.Lerp(aso.volume, 1, Time.deltaTime);
			RotateTowards(player);
			agent.SetDestination(player.position);
		}

		else if ((!playerInSight && is_searching) || playerInRange) { //if player is near but not seen, wander area for time
			if (!is_navigating()) {
				wanderpoint = (Random.onUnitSphere * sightDistance) + lastSightingPosition;
				agent.SetDestination(wanderpoint);
			}			
			aso.volume = Mathf.Lerp(aso.volume, .25f, Time.deltaTime);
		}

		else if(!is_searching) {//wander larger area
			if (!is_navigating()) {
				wanderpoint = (Random.onUnitSphere * searchDistance) + transform.position;
				agent.SetDestination(wanderpoint);
			}

			if (aso.volume > 0) {
				aso.volume = Mathf.Lerp(aso.volume, 0, Time.deltaTime);
			}
		}

		//eye details
		Color eyecolor = eyeMat.GetColor("_EmissionColor");
		Color targetcolor = playerInSight ? Color.red : Color.white;
		int brightval = playerInRange ? 1 : 0;
		eyeMat.SetColor("_EmissionColor", Color.Lerp(eyecolor, targetcolor * brightval, Time.deltaTime));
	}
}
