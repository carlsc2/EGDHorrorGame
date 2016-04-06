using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class lanternSway : MonoBehaviour {

	private CharacterController cc;
	private Rigidbody rb;

	void Start() {
		cc = transform.root.GetComponentInChildren<CharacterController>();
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		rb.AddForce(-cc.velocity/2);
	}
}
