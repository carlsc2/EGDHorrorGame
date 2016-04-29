using UnityEngine;
using System.Collections;
[RequireComponent( typeof(Animator))]
[RequireComponent(typeof(DoorCollider))]
public class OpenFronDoor : MonoBehaviour {
   public GameObject LeftControl;
	public GameObject RightControl;

	public Animator anim;
	public float timer = 3;
	public bool startTimer = false;
	DoorCollider dc;

	// Use this for initialization
	void Start () {
		LeftControl = gameObject.transform.FindChild("LeftControl").gameObject;

		RightControl = gameObject.transform.FindChild("RightControl").gameObject;

		anim = GetComponent<Animator>();
		dc = GetComponent<DoorCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (dc.PlayerHit && Input.GetButtonDown("Use"))
		{
			anim.SetBool("DoorOpen", true);
		  
			startTimer = true;

		}
		if (startTimer && timer > 0)
		{
			timer -= Time.deltaTime;
			print(timer);
		}
		else
		{
			anim.SetBool("DoorOpen", false);
			

		}

		if (!dc.PlayerHit && timer <= 0)
		{
			timer = 3;
			startTimer = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
	   
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && timer <=0 )
		{
			
		}
	}
}
