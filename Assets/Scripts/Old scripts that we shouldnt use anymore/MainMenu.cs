using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public void newGame() {
		Application.LoadLevel("testLevel");
	}

	public void findGame() {

	}

	public void options() {

	}

	public void quit() {
		Application.Quit();
	}
}
