using UnityEngine;
using System.Collections;

public class CollectLantern : MonoBehaviour {

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
		   GameObject  temp =  other.gameObject.transform.FindChild("lanternhinge").gameObject;
		   temp.SetActive(true);
		   Destroy(gameObject);

		}

	}
}
