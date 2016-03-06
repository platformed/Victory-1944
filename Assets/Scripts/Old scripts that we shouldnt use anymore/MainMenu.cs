using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public void newGame() {
		SceneManager.LoadScene("testLevel");
	}

	public void findGame() {

	}

	public void options() {

	}

	public void quit() {
		Application.Quit();
	}
}
