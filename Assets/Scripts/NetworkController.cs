using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkController : NetworkManager {
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			singleton.StopHost();
		}
	}

	public void StartupHost() {
		SetPort();
		singleton.StartHost();
	}

	public void JoinGame() {
		SetIPAddres();
		SetPort();
		singleton.StartClient();
	}

	void SetIPAddres() {
		//Get address from input field
		string address = GameObject.Find("IPAddressField").GetComponent<InputField>().text;

		//Defalt to localhost
		if(address == null || address == "") {
			address = "localhost";
		}

        singleton.networkAddress = address;
    }

	void SetPort() {
		singleton.networkPort = 7777;
	}

	void OnLevelWasLoaded(int level) {
		//Unlock cursor
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		StartCoroutine(SetupMenuButtons());
	}

	IEnumerator SetupMenuButtons() {
		yield return new WaitForSeconds(0.3f);

		GameObject host = GameObject.Find("HostButton");
		if (host != null) {
			host.GetComponent<Button>().onClick.RemoveAllListeners();
			host.GetComponent<Button>().onClick.AddListener(StartupHost);
		}

		GameObject join = GameObject.Find("JoinButton");
		if (join != null) {
			join.GetComponent<Button>().onClick.RemoveAllListeners();
			join.GetComponent<Button>().onClick.AddListener(JoinGame);
		}
	}

	public void Quit() {
		Application.Quit();
	}
}
