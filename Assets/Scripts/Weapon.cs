using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Text ammoText;
	public Transform cam;
	public float damage;
	public float delay;
	public bool continuousFiring;

	public GameObject test;

	bool shooting = false;
	float currentDelay = 0f;
	public int ammo;
	
	void Update () {
		if (currentDelay <= 0) {
			if (continuousFiring) {
				if (Input.GetButtonDown("Fire1")) {
					shooting = true;
					currentDelay = delay;
				}
			} else {
				if (Input.GetButton("Fire1")) {
					shooting = true;
					currentDelay = delay;
				}
			}
		}
	}

	void FixedUpdate() {
		ammoText.text = ammo.ToString();

		test.SetActive(false);

		if (shooting && ammo > 0) {
			shooting = false;

			RaycastHit hit;

			if (Physics.Raycast(cam.position, cam.forward, out hit, 50f)) {
				Player p = hit.transform.GetComponent<Player>();

                if (p != null) {
					p.damage(damage);
				}

				Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);
				Debug.Log("HIT " + hit.transform.name);
			}

			ammo--;

			test.SetActive(true);
		}

		currentDelay--;
	}
}
