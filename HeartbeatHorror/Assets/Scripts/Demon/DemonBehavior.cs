using UnityEngine;
using System.Collections;

public class DemonBehavior : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform player;

	public float fieldOfViewAngle = 20;
	public float sightDistance = 30;
	public bool playerInSight = false;//player is in sight to be chased
	public bool playerInRange = false;//player is in range to be searched for

	private Vector3 wanderpoint;

	public Material eyeMat;

	void OnDrawGizmosSelected() {
		if (playerInSight) {
			Gizmos.color = new Color(1, 1, 0, .25f);
		}
		else {
			Gizmos.color = new Color(1, 0, 0, .25f);
		}
		
		Gizmos.DrawSphere(transform.position, sightDistance);
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	bool check_line_of_sight() {
		//check if player is in line of sight
		Vector3 direction = player.position - transform.position;
		float dist = direction.magnitude;
		float angle = Vector3.Angle(direction, transform.forward);
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
						playerInSight = true;
					}
				}
			}
		}
		else {
			playerInRange = false;
		}
		return true;
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
		check_line_of_sight();
		/*
		if (playerInSight) {//if player in sight, chase the player
			RotateTowards(player);
			agent.SetDestination(player.position);
		}

		else if (playerInRange && !playerInSight && !is_navigating()) { //if player is near but not seen, wander area
			wanderpoint = (Random.onUnitSphere * sightDistance) + transform.position;
			agent.SetDestination(wanderpoint);
		}

		else if(!playerInRange){//wander larger area
			wanderpoint = (Random.onUnitSphere * sightDistance * 5) + transform.position;
			agent.SetDestination(wanderpoint);
		}*/



		//eye details
		Color eyecolor = eyeMat.GetColor("_EmissionColor");
		Color targetcolor = playerInSight ? Color.red : Color.white;
		int brightval = playerInRange ? 1 : 0;
		eyeMat.SetColor("_EmissionColor", Color.Lerp(eyecolor, targetcolor * brightval, Time.deltaTime));
	}
}
