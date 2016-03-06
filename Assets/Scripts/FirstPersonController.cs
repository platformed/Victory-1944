using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	CharacterController controller;
	Transform cam;

	float mouseSensitivity = 1.5f;

	float movementSpeed = 6f;
	float jumpHeight = 5f;

	float verticalVelocity = 0f;

	void Start () {
		//Hides and locks cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		controller = GetComponent<CharacterController>();
		cam = transform.FindChild("Camera");
	}

	void Update () {
		//Unlock mouse
		if (Input.GetKey(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		//Rotation
		float horizontalRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, horizontalRotation, 0);
		
        cam.Rotate(-Input.GetAxis ("Mouse Y"), 0, 0);
		
		//Movement
		float vSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float hSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		//Apply gravity
		if (!controller.isGrounded) {
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}

		//Jump
		if (controller.isGrounded && Input.GetButtonDown("Jump")) {
			verticalVelocity = jumpHeight;
		}

		//Set speed
		Vector3 speed = new Vector3 (hSpeed, verticalVelocity, vSpeed);
		speed = transform.rotation * speed;

		//Move CharacterController
		controller.Move(speed * Time.deltaTime);
	}
}
