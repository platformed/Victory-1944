using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Weapon : NetworkBehaviour {
	public Player player;
	public Transform recoilTransform;
	public Transform cam;
	public float damage;
	public bool continuousFiring;
	public float delay;
	public float recoil;
	public float horizontalRecoil;
	public int ammoPerMagazine;
	public int magazines;
	int ammo;

	Vector3 recoilRot = Vector3.zero;

	bool shooting = false;
	float currentDelay = 0f;

	Text ammoText;

	void Start() {
		if (isLocalPlayer) {
			ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
		}
	}

	void Update() {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.R) && magazines > 0) {
			magazines--;
			ammo = ammoPerMagazine;
			shooting = false;
		}

		if (currentDelay <= 0) {
			if (continuousFiring) {
				if (Input.GetButton("Fire1") && !player.isDead) {
					shooting = true;
					currentDelay = delay;
				}
			} else {
				if (Input.GetButtonDown("Fire1") && !player.isDead) {
					shooting = true;
					currentDelay = delay;
				}
			}
		}

		recoilTransform.localRotation = Quaternion.Lerp(recoilTransform.localRotation, Quaternion.Euler(recoilRot), Time.deltaTime * 10);
	}

	void FixedUpdate() {
		if (!isLocalPlayer) {
			return;
		}

		//Set ammo text
		ammoText.text = ammo + " / " + magazines;

		if (shooting && ammo > 0) {
			shooting = false;

			Shoot();

			ammo--;
		}

		currentDelay--;
	}

	void Shoot() {
		recoilRot -= new Vector3(recoil, Random.Range(-horizontalRecoil, horizontalRecoil), 0);

		RaycastHit hit;
		if (Physics.Raycast(cam.TransformPoint(0f, 0f, 0.5f), cam.forward, out hit, 200)) {
			if (hit.transform.tag == "Player") {
				CmdDamagePlayer(hit.transform.name, damage);
			}
		}
	}

	[Command]
	void CmdDamagePlayer(string identity, float dmg) {
		GameObject.Find(identity).GetComponent<Player>().Damage(damage);
	}
}
