using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
    [Range(1, 10)]
    public int triggerRate;
    public Animator anim;
    
	// Use this for initialization

    void Awake() {
        anim = GetComponent<Animator>();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
            anim.SetTrigger("Door Open");
        
        
        Debug.Log("I've hit something " + other.tag.ToString());
     }

    void OnTriggerExit(Collider other) {

        anim.SetTrigger("Door Open");
        Debug.Log("..and it's gone...");
    }
}
