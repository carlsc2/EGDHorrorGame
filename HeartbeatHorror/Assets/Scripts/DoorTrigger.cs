using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(DoorCollider))]
public class DoorTrigger : MonoBehaviour
{
	[Range(1, 10)]
	public int triggerRate;
	public Animator anim;
	public AnimationCurve curve;
    public AudioSource aSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;
   // SphereCollider sCollider;

    DoorCollider dc;
    public float timer = 3;
    public bool startTimer = false;


	// Use this for initialization

	void Awake()
    {
        //sCollider = GetComponent<SphereCollider>();
        aSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
        dc = GetComponent<DoorCollider>();
		
	}
    void Start() {
        //doorOpen = Resources.Load("Assets/Sounds/door_open.mp3") as AudioClip;
    
    }
    void Update() {
        if (dc.PlayerHit && Input.GetButtonDown("Use"))
        {
            anim.SetBool("Door Open", true);
            //aSource.PlayOneShot(doorOpen);
            startTimer = true;
            
        }
        if (startTimer && timer > 0)
        {
            timer -= Time.deltaTime;
            //print(timer);
        }
        else 
        {
            anim.SetBool("Door Open", false);
            //aSource.PlayOneShot(doorClose);
        
        }

        if (!dc.PlayerHit && timer <=0)
        {
            timer = 3;
            startTimer = false;
        }
    
    }
   public void PlayOpen() {
        aSource.PlayOneShot(doorOpen);
    }

    public void PlayClose()
    {
        aSource.PlayOneShot(doorClose);
    }
	void OnTriggerEnter(Collider other)
	{
        
		
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player" && timer <= 0) {
			Debug.Log("..and it's gone...");
            //timer = 5;
           // startTimer = false;
		}
	}

}
