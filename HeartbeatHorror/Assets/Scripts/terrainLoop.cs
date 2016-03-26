using UnityEngine;
using System.Collections;

public class terrainLoop : MonoBehaviour {

	public float max_dist = 200;

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmp = player.position;
		if (tmp.x > max_dist) {
			tmp.x = -max_dist;
			print("jump" + Time.time);
		}
		else if (tmp.x < -max_dist) {
			tmp.x = max_dist;
			print("jump" + Time.time);
		}
		else if (tmp.z > max_dist) {
			tmp.z = -max_dist;
			print("jump" + Time.time);
		}
		else if (tmp.z < -max_dist) {
			tmp.z = max_dist;
			print("jump" + Time.time);
		}
		player.position = tmp;
	}
}
