using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	CharacterController controller;

	float mouseSensitivity = 1.5f;
	float verticalRotation = 0f;
	float verticalRotationRange = 90f;

	float movementSpeed = 6f;
	float jumpHeight = 5f;

	float verticalVelocity = 0f;

	bool falling = false;

	void Start () {
		//Hides and locks cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		controller = GetComponent<CharacterController>();
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

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -verticalRotationRange, verticalRotationRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//Movement
		float vSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float hSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		//Apply gravity
		if (!controller.isGrounded) {
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}
		if (CollisionFlags.Below == 0) {
			if (!falling) {
				falling = true;
				verticalVelocity = 0f;
			}
		} else {
			if (falling) {
				falling = false;
			}
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
