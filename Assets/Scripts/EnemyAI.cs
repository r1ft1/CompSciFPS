using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Vector3 target;
	public GameObject player;

	void Start () {
		
	}
	
	void Update () {
		target = player.gameObject.GetComponent<Transform>().position;
		transform.LookAt(target);
	}
}
