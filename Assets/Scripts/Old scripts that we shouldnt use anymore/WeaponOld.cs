using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponOld : MonoBehaviour {
	public Transform recoil;
	public Text ammoText;
	public Transform cam;
	public float damage;
	public float delay;
	public bool continuousFiring;
    public float recoilUp = -20.0f;
    public float recoilXAxis = 0.0f;

	Vector3 recoilRot = Vector3.zero;

	bool shooting = false;
	float currentDelay = 0f;

	public int ammoPerMagazine;
	int ammo;
	public int magazines;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.R) && magazines > 0) {
			magazines--;
			ammo = ammoPerMagazine;
			shooting = false;
		}

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

		recoil.localRotation = Quaternion.Lerp(recoil.localRotation, Quaternion.Euler(recoilRot), Time.deltaTime * 10);
	}

	void FixedUpdate() {
		if (ammoText != null) {
			ammoText.text = ammo + " / " + magazines;
		}

		if (shooting && ammo > 0) {
			shooting = false;

			recoilRot -= new Vector3(recoilUp, 0, 0);
			
            RaycastHit hit;
			if (Physics.Raycast(cam.position + cam.forward * 1, cam.forward, out hit, 50f)) {
				PlayerOld p = hit.transform.GetComponent<PlayerOld>();

                if (p != null) {
					p.damage(damage);
				}

				Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);
				Debug.Log("HIT " + hit.transform.name);
			}

			ammo--;
		}

		currentDelay--;
	}
}
