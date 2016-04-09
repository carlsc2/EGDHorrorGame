using System.Collections;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
	[Range(1, 10)]
	public int triggerRate;
	public Animator anim;
	public AnimationCurve curve;

	// Use this for initialization

	void Awake()
	{
		anim = GetComponent<Animator>();
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" ){
		
			anim.SetBool("Door Open",true);
		
		
		Debug.Log("I've hit something " + other.tag.ToString());
	 
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			anim.SetBool("Door Open", false);
			Debug.Log("..and it's gone...");
		}
	}

}
