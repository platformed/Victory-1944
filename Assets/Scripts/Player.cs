using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {
	public GameObject spectator;
	public GameObject cam;

	[SyncVar(hook = "OnHealthChanged")]
	float health = 100;
	GameObject crosshair;
	Text healthText;
	Text respawnText;

	public bool isDead = false;
	float deathTime;

	const float respawnDelay = 10f;

	// Use this for initialization
	void Start() {
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		respawnText = GameObject.Find("RespawnText").GetComponent<Text>();
		crosshair = GameObject.Find("Crosshair");
		SetHealthText();
	}

	// Update is called once per frame
	void Update() {
		CheckCondition();

		if (isDead) {
			deathTime -= Time.deltaTime;
			if (isLocalPlayer) {
				respawnText.text = "Respawning in " + (int)deathTime;
			}
		}
	}

	void CheckCondition() {
		if (health <= 0 && !isDead) {
			isDead = true;
			DisablePlayer();
		}

		if (isDead && deathTime <= 0) {
			isDead = false;
			health = 100f;
			EnablePlayer();
		}
	}

	void DisablePlayer() {
		deathTime = respawnDelay;

		GetComponent<CharacterController>().enabled = false;
		GetComponent<CapsuleCollider>().enabled = false;

		if (isLocalPlayer) {
			spectator.SetActive(true);
			cam.SetActive(false);

			GetComponent<FirstPersonController>().enabled = false;
			crosshair.SetActive(false);
			respawnText.gameObject.SetActive(true);
		}

		//Hide player
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = false;
		}
	}

	void EnablePlayer() {
		GetComponent<CharacterController>().enabled = true;
		GetComponent<CapsuleCollider>().enabled = true;

		if (isLocalPlayer) {
			spectator.SetActive(false);
			spectator.transform.localPosition = new Vector3(0f, 0.6f, 0f);
			spectator.transform.rotation = cam.transform.rotation;
			cam.SetActive(true);

			GetComponent<FirstPersonController>().enabled = true;
			crosshair.SetActive(true);
			respawnText.gameObject.SetActive(false);
		}

		//Show player
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = true;
		}
	}

	public void Damage(float damage) {
		health -= damage;
	}

	void SetHealthText() {
		if (isLocalPlayer) {
			healthText.text = health.ToString() + " Heath";
		}
	}

	void OnHealthChanged(float h) {
		health = h;
		SetHealthText();
	}
}
