using UnityEngine;
using System.Collections;

public class LanternAngle : MonoBehaviour {

	public Transform camera;
	public Vector3 startPos;

	void Start() {
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmp = startPos;
		float h = startPos.y - Mathf.Clamp(camera.localRotation.x*2,-1.5f,.5f);
		tmp.y = h;
		transform.localPosition = Vector3.Slerp(transform.localPosition,tmp,Time.deltaTime * 10);
		print(camera.localRotation);
	}
}
