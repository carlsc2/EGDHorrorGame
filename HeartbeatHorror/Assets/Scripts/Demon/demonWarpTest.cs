using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class demonWarpTest : MonoBehaviour {

	public Vortex v;
	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float vdist = 15 - Mathf.Clamp(Vector3.Distance(transform.position, player.transform.position), 0, 15);
		v.angle =  vdist * Mathf.Sin(Time.time);
	}
}
