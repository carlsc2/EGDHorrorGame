using UnityEngine;
using System.Collections;

public class eatPlayer : MonoBehaviour {

	public static bool killed = false;
	private Transform player;
	public Transform mouth;
	Animator anim;
	private Vector3 eatpos;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void OnTriggerEnter(Collider col) {
		print(col.gameObject);
		if(!killed && col.transform.root.tag == "Player") {
			eat_player();
		}
	
	}

	void Update() {
		if (killed) {
			
			transform.root.position = Vector3.Lerp(transform.root.position, eatpos, Time.deltaTime * 5);

			Vector3 lookPos = player.position - transform.root.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.root.rotation = Quaternion.Slerp(transform.root.rotation, rotation, Time.deltaTime * 2);


			Vector3 lookPos2 = mouth.position - player.position;
			lookPos2.y = 0;
			Quaternion rotation2 = Quaternion.LookRotation(lookPos2);
			player.rotation = Quaternion.Slerp(player.rotation, rotation2, Time.deltaTime * 2);


			//ugly maths here because code not playing nice... but it works
			Transform pcamera = player.GetComponentInChildren<Camera>().transform;
			Vector3 lookPos3 = mouth.position - pcamera.position;
			lookPos3.x = 0;
			Quaternion rotation3 = Quaternion.LookRotation(lookPos3);
			Vector3 tmp = rotation3.eulerAngles;
			tmp.y = 0;
			tmp.z = 0;
			tmp.x += 240;
			rotation3 = Quaternion.Euler(tmp);
			pcamera.localRotation = Quaternion.Slerp(pcamera.localRotation, rotation3, Time.deltaTime * 2);


			if (Mathf.Abs(Vector3.Angle(transform.root.forward, player.forward) - 180) <= 4) {
				anim.SetTrigger("ready");
			}
		}
	}

	void eat_player() {
		player.GetComponent<CharacterController>().enabled = false;
		player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
		player.GetComponentInChildren<LanternAngle>().drop_lantern();
		killed = true;

		eatpos = player.position + player.transform.forward * 2 / 3f;
		eatpos.y = player.position.y - 1.22f;

		anim.SetLayerWeight(1, 0);
		anim.SetLayerWeight(2, 0);
		anim.SetLayerWeight(3, 0);
		anim.SetBool("eatingplayer", true);
		anim.SetFloat("Movement", 0);
		DemonBehavior.mouthtarget = 1;
		anim.speed = 1;
		DemonBehavior.target_speed = 1;
		transform.root.GetComponent<NavMeshAgent>().enabled = false;
	}
}
