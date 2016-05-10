using UnityEngine;
using System.Collections;

public class CollectLantern : MonoBehaviour {

    [SerializeField] private GameObject lanternhinge;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
		   lanternhinge.SetActive(true);
		   Destroy(gameObject);

		}

	}
}
