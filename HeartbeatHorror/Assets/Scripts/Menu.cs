using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {

    private Canvas can;
    private CharacterController player;
   
    public bool active = true;
	// Use this for initialization
	void Start () {
        can = GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            active = !active;
        }
        if (active)
        {
            Time.timeScale = 0;
            can.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            can.enabled = false;
        }

	}
    public void QuitGame(){
        if (!Application.isEditor)
        {
            Application.Quit();
        }
        else {
            print("Quit!");
        }
    }
  }
