using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public AudioSource beta;
    public Transform targetTransform;
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        beta.volume = 1.0f - (Vector3.Distance(transform.position, targetTransform.position) / 10);
        float step = Time.deltaTime * speed;
        Vector3 targetDir = targetTransform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        transform.LookAt(targetTransform);

        if (Vector3.Angle(targetTransform.forward, transform.position - targetTransform.transform.position) < 10.0f) {
       
        }


    }
}
