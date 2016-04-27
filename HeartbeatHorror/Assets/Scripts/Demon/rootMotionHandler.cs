using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class rootMotionHandler : MonoBehaviour {

	private Transform troot;
	private Animator anim;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		troot = transform.root;
		agent = troot.GetComponent<NavMeshAgent>();
	}

	void OnAnimatorMove() {
		//update root body position
		Vector3 newPosition = troot.position;
		newPosition += anim.GetFloat("WalkSpeed") * anim.deltaPosition * Time.deltaTime;
		troot.position = newPosition;

		//need this to apply root motion for navmesh agent

        
        float dtime = Mathf.Approximately(Time.deltaTime,0) ? 1 : Time.deltaTime; 
        agent.velocity = anim.deltaPosition / dtime;
                
        

	}
}
