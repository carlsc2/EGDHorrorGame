using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {

    private Canvas can;
    private bool active;
	// Use this for initialization
	void Start () {
        can = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            active = !active;
        }
        if (active)
        {
            can.enabled = true;
        }
        else
        {
            can.enabled = false;
        }

	}
}
