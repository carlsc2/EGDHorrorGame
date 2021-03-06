﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof( AudioSource))]
public class TriggerVolume : MonoBehaviour {
	public AudioClip sound;
	public AudioSource source;

	public UnityEvent triggerEvent;
	
	
   // SphereCollider col;
	[Range(1, 10)]
	public int triggerRate;
	
	public bool playAfterDelay;
	[Range(1,5)]
	public float delay;

	public float minimumInterval = 0; //minimum time interval between sound plays

	private float last_playtime = -1000;

	
	public bool playOnEnter;
	public bool eventTrigger;
	public bool playOnRate;
	public bool playSound;
	public bool playOnce;
	private bool triggered;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	   // triggerEvent.AddListener(source.Play);
	}
	
	IEnumerator OnTriggerEnter(Collider other)
	{
		float temp = delay;
		if (other.gameObject.tag == "Player") {
			if (playAfterDelay) {
				yield return new WaitForSeconds(delay);
			}

			if (Time.time - last_playtime > minimumInterval) {
				last_playtime = Time.time;
				if (!playOnce) {
					if (playOnEnter) {
						source.PlayOneShot(sound);
					}
					if (eventTrigger) {
						triggerEvent.Invoke();
					}

					if (playOnRate) {
						if (HBListener.Instance.avgPulse > HBListener.Instance.base_rate * (1 + triggerRate / 100)) {

							if (playSound) {
								source.PlayOneShot(sound);
							}

							if (eventTrigger) {
								triggerEvent.Invoke();
							}
						}
					}

				}
				else if (playOnce && !triggered) {
					if (playOnEnter) {
						source.PlayOneShot(sound);
					}
					if (eventTrigger) {
						triggerEvent.Invoke();
					}

					if (playOnRate) {
						if (HBListener.Instance.avgPulse > HBListener.Instance.base_rate * (1 + triggerRate / 100)) {

							if (playSound) {
								source.PlayOneShot(sound);
							}

							if (eventTrigger) {
								triggerEvent.Invoke();
							}
						}
					}
					triggered = true;
				}
			}
		}
	}


	void TriggerEvent() {
	   // triggeredObject.GetComponent<Trigger>();

	}
}
