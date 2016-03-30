using UnityEngine;
using System.Collections;

public class TriggerVolume : MonoBehaviour {
    public AudioClip sound;
    public AudioSource source;
    
    
    
    BoxCollider col;
    [Range(0, 10)]
    public int triggerRate;
    
    public bool playSound;
    public bool eventTrigger;


    private bool triggered;
	// Use this for initialization
	void Start () {
        col = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider other) {
        if (playSound)
        {
            source.PlayOneShot(sound);
        }

        if(eventTrigger){

            TriggerEvent();
        }

    }


    void TriggerEvent() { 
    }
}
