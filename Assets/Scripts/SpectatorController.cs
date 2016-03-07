using UnityEngine;
using System.Collections;

public class SpectatorController : MonoBehaviour {
	CharacterController controller;

	float mouseSensitivity = 1.5f;
	float movementSpeed = 6f;

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSensitivity, 0, 0);
		transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * mouseSensitivity);

		//Movement
		float vSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float hSpeed = Input.GetAxis("Horizontal") * movementSpeed;

		//Set speed
		Vector3 speed = new Vector3(hSpeed, 0, vSpeed);
		speed = transform.rotation * speed;

		//Move CharacterController
		controller.Move(speed * Time.deltaTime);
	}
}
