using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class TDMGamemode : NetworkBehaviour {
	[SyncVar]
	public int teamAPoints;
	[SyncVar]
	public int teamBPoints;

	[SyncVar]
	public int teamAPlayers;
	[SyncVar]
	public int teamBPlayers;

	public Text pointsText;

	void Start() {
		teamAPoints = 0;
		teamBPoints = 0;
	}

	void Update() {
		Debug.Log("Set points text");
		pointsText.text = teamAPoints + " - " + teamBPoints;
	}

	[Command]
	public void CmdAddPoint(Team team) {
		if (team == Team.A) {
			teamAPoints++;
		} else {
			teamBPoints++;
		}
	}

	[Command]
	public void CmdAddPlayer(Team team) {
		if (team == Team.A) {
			teamAPlayers++;
		} else {
			teamBPlayers++;
		}
	}
}
