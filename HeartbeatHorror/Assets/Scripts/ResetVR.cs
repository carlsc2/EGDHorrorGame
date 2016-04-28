using UnityEngine;
using System.Collections;
using UnityEngine.VR;
public class ResetVR : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.VR.InputTracking.Recenter();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Reset"))
        {
            UnityEngine.VR.InputTracking.Recenter();
        }
	}
}

