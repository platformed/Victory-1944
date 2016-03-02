using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Text ammoText;
	public Transform cam;
	public float damage;
	public float delay;
	public bool continuousFiring;
    public float RecoilUp = -20.0f;
    public float Recoil2 = 0.0f;
    public float reloadTime = 1.0f;

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
	}

	void FixedUpdate() {
		ammoText.text = ammo + " / " + magazines;

		if (shooting && ammo > 0) {
			shooting = false;
			
            cam.Rotate(RecoilUp, 0, 0);
			
            RaycastHit hit;
			if (Physics.Raycast(cam.position + cam.forward * 1, cam.forward, out hit, 50f)) {
				Player p = hit.transform.GetComponent<Player>();

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
