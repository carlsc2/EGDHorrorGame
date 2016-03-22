using UnityEngine;
using System.Collections;

public class DemonBehavior : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform player;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(player.position);
	}
}
