using UnityEngine;
using System.Collections;

[RequireComponent (typeof( NavMeshAgent))]
public class AIAgentHandler : MonoBehaviour {
    public AINavController.PathLocation patrolArea;
    private NavMeshAgent navMeshAgent;
    public bool moveToEnd;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void MoveToPoint(){
        navMeshAgent.destination = AINavController.Instance.RequestPos(patrolArea,moveToEnd);

        //NavMeshPath path = new NavMeshPath();
        //navMeshAgent.CalculatePath(AINavController.Instance.RequestPos(patrolArea), path);
    }
}
