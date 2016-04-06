using UnityEngine;
using System.Collections;

[RequireComponent (typeof( NavMeshAgent))]

public class AIAgentHandler : MonoBehaviour {
    public AINavController.PathLocation patrolArea;
    private NavMeshAgent navMeshAgent;
    private MeshRenderer[] meshRenderers;
    public bool moveToEnd;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }
	// Use this for initialization
	void Start () {
        disableRender();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void MoveToPoint(){
       enableRender();
       navMeshAgent.destination = AINavController.Instance.RequestPos(patrolArea,moveToEnd);
       //disableRender(); 
       //NavMeshPath path = new NavMeshPath();
        //navMeshAgent.CalculatePath(AINavController.Instance.RequestPos(patrolArea), path);
    }

   void enableRender()
   {

       foreach (MeshRenderer mesh in meshRenderers )
       {
           mesh.enabled = true;
       }
   }

   void disableRender() {
       foreach (MeshRenderer mesh in meshRenderers)
       {
           mesh.enabled = false;
       }
   }
}
