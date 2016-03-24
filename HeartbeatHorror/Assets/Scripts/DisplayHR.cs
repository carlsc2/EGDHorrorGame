using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplayHR : MonoBehaviour {
	public Text rate;
	public HBListener listen;
	// Use this for initialization
	void Start () {
		rate = GameObject.FindGameObjectWithTag("test").GetComponent<Text>();
		listen = GameObject.FindGameObjectWithTag("Listener").GetComponent<HBListener>();
	}
	
	// Update is called once per frame
	void Update () {
		rate.text = "baseline: " + listen.base_rate + "\nbpm: " + listen.outPulse;
	
	}
}
