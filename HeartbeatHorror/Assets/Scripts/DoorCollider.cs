using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour {


    public bool PlayerHit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other){
        if (other.tag == "Player")
        {
            PlayerHit = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            PlayerHit = false;
        }
    }
}
