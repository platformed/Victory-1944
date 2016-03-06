using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkTransform : NetworkBehaviour {
	public bool transmitPos = true;
	public bool transmitRot = true;

	[SyncVar]
	Vector3 syncPos;
	[SyncVar]
	Quaternion syncRot;

	Vector3 lastPos;
	float posThreshold = 0.1f;

	Quaternion lastRot;
	float rotThreshold = 5f;

	float smoothingRate = 15;

	void Update() {
		if (transmitPos) {
			LerpPosition();
		}

		if (transmitRot) {
			LerpRotation();
		}
	}

	void FixedUpdate() {
		if (transmitPos) {

			TransmitPos();
		}

		if (transmitRot) {
			TransmitRot();
		}
	}

	void LerpPosition() {
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * smoothingRate);
		}
	}

	void LerpRotation() {
		if (!isLocalPlayer) {
			transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * smoothingRate);
		}
	}

	[Command]
	void CmdProvidePosToServer(Vector3 pos) {
		syncPos = pos;
	}

	[Command]
	void CmdProvideRotToServer(Quaternion rot) {
		syncRot = rot;
	}

	[Client]
	void TransmitPos() {
		if (isLocalPlayer && Vector3.Distance(transform.position, lastPos) > posThreshold) {
			CmdProvidePosToServer(transform.position);
			lastPos = transform.position;
		}
	}

	[Client]
	void TransmitRot() {
		if (isLocalPlayer && Quaternion.Angle(transform.rotation, lastRot) > rotThreshold) {
			CmdProvideRotToServer(transform.rotation);
			lastRot = transform.rotation;
		}
	}
}
