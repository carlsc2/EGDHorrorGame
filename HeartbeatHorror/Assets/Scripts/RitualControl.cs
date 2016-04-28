using UnityEngine;
using System.Collections;

public class RitualControl : MonoBehaviour {

	public RitualSlot slot1, slot2, slot3, slot4;
	private bool completed = false;

	public ParticleSystem rift;
	public ParticleSystem implosion;

	public GameObject dummymonster;

	// Update is called once per frame
	void Update () {
		if(!completed && slot1.itemInSlot && slot2.itemInSlot && slot3.itemInSlot && slot4.itemInSlot) {
			completed = true;
			StartCoroutine(complete_ritual());
		}
	}

	IEnumerator complete_ritual() {

		dummymonster.SetActive(true);

		yield return new WaitForSeconds(1);

		//open rift
		rift.randomSeed = 1;
		float i = 0;
		for(float t=0; t < 5; t += Time.deltaTime) {
			if(i < 3) {
				i = t * 3 / 5;
				rift.startSize = i;
				rift.Simulate(Time.deltaTime, false, false);
			}
			yield return new WaitForEndOfFrame();
		}
		rift.Play();
		dummymonster.GetComponentInChildren<Animator>().SetTrigger("die");


		//implode

		//implosion.Play(); //this doesnt work for some god awful reason

		//workaround.... use playonawake
		implosion.gameObject.SetActive(true);

		yield return new WaitForSeconds(1.867f);
		dummymonster.SetActive(false);

		

		rift.randomSeed = 1;
		//rift.startLifetime = 3 * 1.867f/5;
		for (float t=0; t < 0.5f; t += Time.deltaTime) {
			rift.startSize = Mathf.Lerp(3, 0, t / 0.5f);
			rift.Simulate(Time.deltaTime * 3 * 5 / 0.5f, false, false);
			yield return new WaitForEndOfFrame();
		}
		rift.startSize = 0;
		rift.Play();

		yield return null;
	}
}
