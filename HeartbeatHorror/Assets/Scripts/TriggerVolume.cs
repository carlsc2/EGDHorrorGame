using UnityEngine;
using System.Collections;

public class TriggerVolume : MonoBehaviour {
    public AudioClip sound;
    public AudioSource source;

    
    
    BoxCollider col;
    [Range(1, 10)]
    public int triggerRate;
    
    public bool playOnEnter;
    public bool eventTrigger;
    public bool playOnRate;


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
        if (playOnEnter)
        {
            source.PlayOneShot(sound);
        }
        if(eventTrigger){
        TriggerEvent();
        }

        if (playOnRate)
        {
            if (HBListener.Instance.avgPulse > HBListener.Instance.base_rate * (1 + triggerRate / 100))
            {
                source.PlayOneShot(sound);
            }
        }
        
    }


    void TriggerEvent() { 
    }
}
