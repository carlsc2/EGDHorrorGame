using UnityEngine;
using System.Collections;

public class LanternFlameControl : MonoBehaviour {

	private Transform monster;
	public float nearDist = 5;
	public float farDist = 20;
	public GameObject flameObj;
	private Material candleMat;

	public float alertdist = 20;


	void Awake () {
		candleMat = flameObj.GetComponent<MeshRenderer>().material;
		candleMat.SetFloat("_Color", 0);
		candleMat.SetFloat("_Green", 0);
		monster = GameObject.FindGameObjectWithTag("Monster").transform;
	}
	
	void Update () {
		float itemdist = alertdist;
		foreach(Collider col in  Physics.OverlapSphere(transform.position, alertdist)) {
			if(col.GetComponent<Item>() != null) {
				itemdist = Vector3.Distance(transform.position, col.transform.position);
				break;
			}
		}


		if(monster != null) {
			float dist = Vector3.Distance(transform.position, monster.position);
			float val = 1 - ((dist - nearDist) / (farDist - nearDist));
			val = Mathf.Clamp01(val);
			candleMat.SetFloat("_Color", val);
			candleMat.SetFloat("_Green", 1 - Mathf.Clamp01(itemdist /alertdist));
		}
	}
}
