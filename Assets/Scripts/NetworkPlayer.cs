﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class NetworkPlayer : NetworkBehaviour {
	[SyncVar]
	public string playerUniqueIdentity;
	NetworkInstanceId playerNetID;

	public override void OnStartLocalPlayer() {
		GetNetIdentity();
		SetIdentity();
	}

	void Start() {
		if (isLocalPlayer) {
			GameObject.Find("Scene Camera").SetActive(false);

			GetComponent<CharacterController>().enabled = true;
			GetComponent<FirstPersonController>().enabled = true;

			transform.FindChild("Camera").gameObject.SetActive(true);
		}
	}

	void Update() {
		if(transform.name == "Player(Clone)" || transform.name == "") {
			SetIdentity();
		}
	}

	[Client]
	void GetNetIdentity() {
		playerNetID = GetComponent<NetworkIdentity>().netId;
		CmdTellServerIdentity(MakeUniqueIdentity());
	}
	
	void SetIdentity() {
		if (!isLocalPlayer) {
			transform.name = playerUniqueIdentity;
		} else {
			transform.name = MakeUniqueIdentity();
		}
	}

	string MakeUniqueIdentity() {
		return "Player " + playerNetID.ToString();
	}

	[Command]
	void CmdTellServerIdentity(string name) {
		playerUniqueIdentity = name;
	}
}
