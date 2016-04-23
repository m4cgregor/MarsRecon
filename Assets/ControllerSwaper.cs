using UnityEngine;
using System.Collections;
using UnityStandardAssets;

public class ControllerSwaper : MonoBehaviour {

	FirstPersonController groundController;
	CharacterController characterController;
	Rigidbody body;
	JetPack airController;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody> ();
		groundController = GetComponent<FirstPersonController> ();
		characterController = GetComponent<CharacterController> ();
		airController = GetComponent<JetPack> ();
	}

	void OnDisable() {
		body.isKinematic = false;
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		characterController.enabled = false;
		airController.enabled = false;
		groundController.enabled = false;
	}

	void OnEnable() {
		body.isKinematic = true;
		body.constraints = 0;
		airController.enabled = false;
		groundController.enabled = true;
		characterController.enabled = true;
		characterController.Move (new Vector3(0, -1, 0));
	}

	void OnTriggerEnter(Collider col) {
		if (!enabled)
			return;
		if (col.tag == "Terrain") {
			body.isKinematic = true;
			body.constraints = 0;
			airController.enabled = false;
			groundController.enabled = true;
			characterController.enabled = true;
			characterController.Move (new Vector3(0, -1, 0));
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
