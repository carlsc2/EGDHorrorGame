using UnityEngine;
using System.Collections;

public class LanternFlameControl : MonoBehaviour {

	private Transform monster;
	public float nearDist = 5;
	public float farDist = 20;
	public Material candleMat;

	void Start () {
		monster = GameObject.FindGameObjectWithTag("Monster").transform;
	}
	
	void Update () {
		float dist = Vector3.Distance(transform.position, monster.position);
		float val = 1 - ((dist - nearDist) / (farDist - nearDist));
		val = Mathf.Clamp01(val);
		candleMat.SetFloat("_Color", val);
	}
}
