using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour {
	public GameObject[] weapons;
	int currentWeapon = 0;
	
	void Start () {
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			currentWeapon = 0;
			switchWeapon();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			currentWeapon = 1;
			switchWeapon();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			currentWeapon = 2;
			switchWeapon();
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll > 0f) {
			currentWeapon--;
			if (currentWeapon < 0) {
				currentWeapon = 2;
			}
			switchWeapon();
		}
		if (scroll < 0f) {
			currentWeapon++;
			if (currentWeapon > 2) {
				currentWeapon = 0;
			}
			switchWeapon();
		}
	}

	void switchWeapon() {
		for (int i = 0; i < weapons.Length; i++) {
			if (i == currentWeapon) {
				weapons[i].SetActive(true);
			} else {
				weapons[i].SetActive(false);
			}
		}
	}
}
