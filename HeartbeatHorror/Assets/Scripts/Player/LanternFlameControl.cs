using UnityEngine;
using System.Collections;

public class LanternFlameControl : MonoBehaviour {

	private Transform monster;
	public float nearDist = 5;
	public float farDist = 20;
	public GameObject flameObj;
	private Material candleMat;


	void Awake () {
		candleMat = flameObj.GetComponent<MeshRenderer>().material;
		candleMat.SetFloat("_Color", 0);
		monster = GameObject.FindGameObjectWithTag("Monster").transform;
	}
	
	void Update () {
		if(monster != null) {
			float dist = Vector3.Distance(transform.position, monster.position);
			float val = 1 - ((dist - nearDist) / (farDist - nearDist));
			val = Mathf.Clamp01(val);
			candleMat.SetFloat("_Color", val);
		}
	}
}
