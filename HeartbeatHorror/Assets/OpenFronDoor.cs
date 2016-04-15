using UnityEngine;
using System.Collections;

public class OpenFronDoor : MonoBehaviour {
   public GameObject LeftControl;
    public GameObject RightControl;
    


	// Use this for initialization
	void Start () {
        LeftControl = gameObject.transform.FindChild("LeftControl").gameObject;

        RightControl = gameObject.transform.FindChild("RightControl").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
}
