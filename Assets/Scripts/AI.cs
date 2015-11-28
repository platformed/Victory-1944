using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public Transform player;
	public NavMeshAgent a;

	void Start () {
	
	}
	
	void Update () {
		a.SetDestination(player.position);
	}
}
