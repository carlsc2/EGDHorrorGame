using UnityEngine;
using System.Collections;

public class eatPlayer : MonoBehaviour {

	public static bool killed = false;
	private Transform player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Use this for initialization
	void OnCollisionEnter(Collision col) {
		if(!killed && col.collider.transform.root.tag == "Player") {
			eat_player();
		}
	
	}

	void Update() {
		if (killed) {
			transform.root.LookAt(player);
		}
	}

	void eat_player() {
		player.GetComponent<CharacterController>().enabled = false;
		player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
		//Rigidbody rb = player.GetComponent<Rigidbody>();
		//rb.isKinematic = false;
		//rb.AddForce(-player.forward * 20);
		player.GetComponentInChildren<LanternAngle>().drop_lantern();
		killed = true;


		Animator anim = GetComponent<Animator>();
		anim.SetLayerWeight(3, 0);
		anim.SetBool("eatingplayer", true);
		anim.SetFloat("Movement", 0);
		DemonBehavior.mouthtarget = 1;
		DemonBehavior.target_speed = 0;
		
		transform.root.GetComponent<NavMeshAgent>().enabled = false;
		//transform.root.GetComponent<DemonBehavior>().enabled = false;
		transform.position = player.position + player.forward;
		transform.root.LookAt(player); 
	}
}
