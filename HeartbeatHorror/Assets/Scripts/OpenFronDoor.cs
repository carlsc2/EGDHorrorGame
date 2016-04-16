using UnityEngine;
using System.Collections;
[RequireComponent( typeof(Animator))]
public class OpenFronDoor : MonoBehaviour {
   public GameObject LeftControl;
    public GameObject RightControl;

    public Animator anim;
    


	// Use this for initialization
	void Start () {
        LeftControl = gameObject.transform.FindChild("LeftControl").gameObject;

        RightControl = gameObject.transform.FindChild("RightControl").gameObject;

        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("DoorOpen", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("DoorOpen", false);
        }
    }
}
