using UnityEngine;
using System.Collections;
using UnityStandardAssets;

public class ControllerSwaper : MonoBehaviour {

	FirstPersonController groundController;
	CharacterController characterController;
	Rigidbody body;
	JetPack airController;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		groundController = GetComponent<FirstPersonController> ();
		characterController = GetComponent<CharacterController> ();
		airController = GetComponent<JetPack> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Terrain") {
			body.isKinematic = true;
			body.constraints = 0;
			airController.enabled = false;
			groundController.enabled = true;
			characterController.enabled = true;
			characterController.Move (new Vector3(0, -1, 0));
			//characterController.Move (new Vector3(0, 0.01f, 0));
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Terrain") {
			body.isKinematic = false;
			airController.enabled = true;
			body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			groundController.enabled = false;
			characterController.enabled = false;
		}
	}
}
