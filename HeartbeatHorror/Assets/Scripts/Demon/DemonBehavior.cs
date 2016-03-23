using UnityEngine;
using System.Collections;

public class DemonBehavior : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform player;

	public float fieldOfViewAngle = 20;
	public float sightDistance = 30;
	public bool playerInSight = false;
	public bool playerInRange = false;

	public Material eyeMat;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	bool check_line_of_sight() {
		//check if player is in line of sight
		//if (Vector3.Angle(transform.forward,player.position) < LOS_angle) {

		//}
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

						// Set the last global sighting is the players current position.
						//lastPlayerSighting.position = player.transform.position;
					}
				}
			}
		}
		else {
			playerInRange = false;
		}
		return true;
	}

	
	// Update is called once per frame
	void Update () {
		//agent.SetDestination(player.position);
		check_line_of_sight();
		Color eyecolor = eyeMat.GetColor("_EmissionColor");
		Color targetcolor = playerInSight ? Color.red : Color.white;
		int brightval = playerInRange ? 1 : 0;
		eyeMat.SetColor("_EmissionColor", Color.Lerp(eyecolor, targetcolor * brightval, Time.deltaTime));
	}
}
