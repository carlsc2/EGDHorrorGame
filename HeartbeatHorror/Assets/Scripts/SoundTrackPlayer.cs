using UnityEngine;
using System.Collections;




public class SoundTrackPlayer : MonoBehaviour {

    public bool playOnStart = true;

    public AudioSource track;

    public float detuneRange = 1.0f;
    public float detuneSpeed = 0.5f;
    public float detuneIntensity = 1.0f;


    float delta = 0.0f;

    // Use this for initialization
    void Start () {


        if (playOnStart) {
            if (!track.isPlaying)
                track.Play();
        }
	
	}

    // Update is called once per frame
    void Update()
    {
        track.pitch = (1.0f + delta);

        delta += Random.Range(-detuneIntensity, detuneIntensity) * Time.deltaTime * detuneSpeed;
    }
}
 