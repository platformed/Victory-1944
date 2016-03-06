using UnityEngine;
using System.Collections;

public class PlayerOld : MonoBehaviour {
	float health = 100f;
	//bool dead = false;
	
	void Update () {
		if (health <= 0f) {
			kill();
		}
	}

	public void damage(float amount) {
		health -= amount;
	}

	void kill() {
		//dead = true;
		Destroy(gameObject);
	}
}
