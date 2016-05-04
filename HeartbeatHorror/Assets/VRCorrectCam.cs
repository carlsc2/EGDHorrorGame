using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VRCorrectCam : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.VR.VRDevice.isPresent) {
            Vector3 newRot = new Vector3(0,0,0); 
            newRot.y = transform.rotation.eulerAngles.y;
            transform.parent.rotation = Quaternion.Euler(newRot);
            newRot = transform.rotation.eulerAngles;
            newRot.y = 0;
            transform.rotation = (Quaternion.Euler(newRot));
        }
	}
}
