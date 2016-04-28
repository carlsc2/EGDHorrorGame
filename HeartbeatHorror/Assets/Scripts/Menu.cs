using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {

	private Canvas can;
	private CharacterController player;

	public GameObject pauseobj;
   
	public bool active = true;
	// Use this for initialization
	void Start () {
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
			pauseobj.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			pauseobj.SetActive(false);
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
