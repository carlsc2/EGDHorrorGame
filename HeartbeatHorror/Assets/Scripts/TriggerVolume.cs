using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof( AudioSource))]
public class TriggerVolume : MonoBehaviour {
    public AudioClip sound;
    public AudioSource source;

    public UnityEvent triggerEvent;
    
    
   // SphereCollider col;
    [Range(1, 10)]
    public int triggerRate;
    
    public bool playOnEnter;
    public bool eventTrigger;
    public bool playOnRate;

    public bool playSound;

    public bool playOnce;
    private bool triggered;

    void Awake()
    {
        source = GetComponent<AudioSource>();
       // triggerEvent.AddListener(source.Play);
    }
	// Use this for initialization
	void Start () {
       // col = GetComponent<SphereCollider>();

	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void OnTriggerEnter(Collider other)
    {
        if (playOnce && !triggered)
        {
            if (playOnEnter)
            {
                source.PlayOneShot(sound);
            }
            if (eventTrigger)
            {
                triggerEvent.Invoke();
            }

            if (playOnRate)
            {
                if (HBListener.Instance.avgPulse > HBListener.Instance.base_rate * (1 + triggerRate / 100))
                {

                    if (playSound)
                    {
                        source.PlayOneShot(sound);
                    }

                    if (eventTrigger)
                    {
                        triggerEvent.Invoke();
                    }
                }
            }

            triggered = true;
        }
        else
        {
            if (playOnEnter)
            {
                source.PlayOneShot(sound);
            }
            if (eventTrigger)
            {
                triggerEvent.Invoke();
            }

            if (playOnRate)
            {
                if (HBListener.Instance.avgPulse > HBListener.Instance.base_rate * (1 + triggerRate / 100))
                {

                    if (playSound)
                    {
                        source.PlayOneShot(sound);
                    }

                    if (eventTrigger)
                    {
                        triggerEvent.Invoke();
                    }
                }
            }
        }
    }


    void TriggerEvent() {
       // triggeredObject.GetComponent<Trigger>();

    }
}
