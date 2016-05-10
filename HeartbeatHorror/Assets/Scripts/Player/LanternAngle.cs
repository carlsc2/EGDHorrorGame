using UnityEngine;
using System.Collections;

public class LanternAngle : MonoBehaviour {

	public Vector3 startPos;
    public Vector3 snapshotStartPos;
	private Transform cam;

	private bool dropped = false;

	public bool do_drop = false;

    public float heldHeight = .5f;
    public float lanternLerp = .1f;
    public float lanternDistance = .5f;

	void Start() {
		snapshotStartPos = startPos = transform.localPosition;
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dropped) {
			
			Vector3 tmp = startPos;
			float h;
			if (UnityEngine.VR.VRDevice.isPresent) {//reverse the direction in VR
				h = startPos.y + Mathf.Clamp(cam.localRotation.x * 2, -1.5f, lanternLerp);
			}
			else {
				h = startPos.y - Mathf.Clamp(cam.localRotation.x * 2, -1.5f, lanternLerp);
			}
			tmp.y = h + heldHeight;
			transform.localPosition = Vector3.Slerp(transform.localPosition, tmp, Time.deltaTime * 10);

			if (do_drop) {
				drop_lantern();
			}
		}
	}

	public void drop_lantern() {
		dropped = true;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.isKinematic = false;
		Vector3 curpos = transform.position;//.root.position + transform.localPosition;
		transform.parent = null;
		transform.position = curpos;

	}
}
