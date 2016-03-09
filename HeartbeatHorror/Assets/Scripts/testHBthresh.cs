using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testHBthresh : MonoBehaviour {

	public AudioClip uscared;
	private AudioSource aso;

	void Start() {
		aso = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if(HBListener.Instance.base_rate != -1 && HBListener.Instance.avgPulse > HBListener.Instance.base_rate * 1.07f) {//if 7% increase
			//print("You scared bro?");
			if (!aso.isPlaying) {
				aso.PlayOneShot(uscared);
			}
		}
	
	}

}
