using UnityEngine;
using System.Collections;

public class LanternFlameControl : MonoBehaviour {

	private Transform monster;
	public float nearDist = 5;
	public float farDist = 20;
	public GameObject flameObj;
	private Material candleMat;

	public float alertdist = 20;

	private float targetgreen;

	public bool do_green_check = false;


	void Awake () {
		candleMat = flameObj.GetComponent<MeshRenderer>().material;
		candleMat.SetFloat("_Color", 0);
		candleMat.SetFloat("_Green", 0);
		monster = GameObject.FindGameObjectWithTag("Monster").transform;
		if (do_green_check) {
			StartCoroutine(check_item_dist());
		}
	}

	IEnumerator check_item_dist() {
		while (true) {
			float itemdist = alertdist;
			foreach (Collider col in Physics.OverlapSphere(transform.position, alertdist)) {
				if (col.GetComponent<Item>() != null) {
					itemdist = Vector3.Distance(transform.position, col.transform.position);
					break;
				}
			}
			targetgreen = 1 - Mathf.Clamp01(itemdist / alertdist);

			yield return new WaitForSeconds(.5f);
		}
	}
	
	void Update () {

		float curval = candleMat.GetFloat("_Green");
		candleMat.SetFloat("_Green", Mathf.MoveTowards(curval,targetgreen,Time.deltaTime));

		if (monster != null) {
			float dist = Vector3.Distance(transform.position, monster.position);
			float val = 1 - ((dist - nearDist) / (farDist - nearDist));
			val = Mathf.Clamp01(val);
			candleMat.SetFloat("_Color", val);
			
		}
	}
}
