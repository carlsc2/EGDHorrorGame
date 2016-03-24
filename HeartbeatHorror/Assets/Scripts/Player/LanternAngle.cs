using UnityEngine;
using System.Collections;

public class LanternAngle : MonoBehaviour {

	public Vector3 startPos;
	private Transform cam;

	void Start() {
		startPos = transform.localPosition;
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmp = startPos;
		float h = startPos.y - Mathf.Clamp(cam.localRotation.x*2,-1.5f,.5f);
		tmp.y = h;
		transform.localPosition = Vector3.Slerp(transform.localPosition,tmp,Time.deltaTime * 10);
	}
}
