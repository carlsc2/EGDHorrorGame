using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]
public class DoorTrigger : MonoBehaviour
{
	[Range(1, 10)]
	public int triggerRate;
	public Animator anim;
	public AnimationCurve curve;
    public AudioSource aSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    SphereCollider colider;

	// Use this for initialization

	void Awake()
    {
        colider = GetComponent<SphereCollider>();
        aSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		
	}
    void Start() {
        doorOpen = Resources.Load("Assets/Sounds/door_open.mp3") as AudioClip;
    
    }
    void Update() { 
    
    
    }
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player"  ){
		
			anim.SetBool("Door Open",true);
            aSource.PlayOneShot(doorOpen);
		
		
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
