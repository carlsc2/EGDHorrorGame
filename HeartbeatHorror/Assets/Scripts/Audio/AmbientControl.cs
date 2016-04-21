using UnityEngine;
using System.Collections;

public class AmbientControl : MonoBehaviour {

	private AudioSource monster_close;
	private AudioSource ambient;

	// Use this for initialization
	void Start () {
		monster_close = GameObject.FindGameObjectWithTag("Monster").GetComponent<DemonBehavior>().aso;
		ambient = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		ambient.volume = 1 - (monster_close.volume*4);
	}
}
