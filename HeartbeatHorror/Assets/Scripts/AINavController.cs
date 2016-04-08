using UnityEngine;
using System.Collections.Generic;

public class AINavController :Singleton<AINavController>{

    [System.Serializable]
    public class PathNavPointBinder {
        public PathLocation pathingArea;
        public List<GameObject> navPoints;

    }

    public enum PathLocation { 
        MainFloor = 0, 
        FrontLeft = 1
    }


   
    public List<PathNavPointBinder> navPoints;
    public Dictionary<PathLocation, List<GameObject>> pathLookUp;

    void Awake() {

        pathLookUp = new Dictionary<PathLocation, List<GameObject>>();
        foreach (PathNavPointBinder item in navPoints)
        {
            pathLookUp.Add(item.pathingArea, item.navPoints);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 RequestPos(PathLocation pathLoc, bool start) {

        if (start)
        {
            return pathLookUp[pathLoc][0].transform.position;
        
        }
        else
        {
            return pathLookUp[pathLoc][1].transform.position;
    
        }
    }

    public bool RequestPath(Vector3 startPoint, Vector3 endPoint, ref NavMeshPath path, int areaMask = NavMesh.AllAreas){
        return NavMesh.CalculatePath(startPoint, endPoint, areaMask, path);
    }
}
