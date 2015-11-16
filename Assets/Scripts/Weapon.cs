using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Transform cam;
	public float damage;
	public float delay;

	public GameObject test;

	bool shooting = false;
	float currentDelay = 0f;
	
	void Update () {
		if (Input.GetButton("Fire1") && currentDelay <= 0) {
			shooting = true;
			currentDelay = delay;
		}
	}

	void FixedUpdate() {
		test.SetActive(false);

		if (shooting) {
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

			test.SetActive(true);
		}

		currentDelay--;
	}
}
