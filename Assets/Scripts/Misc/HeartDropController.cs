﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDropController : MonoBehaviour {

	public int health = 1;
	public GameObject hitmarker;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Player") {
			col.gameObject.GetComponent<PlayerController>().GetHealth(this.health);
			Instantiate(hitmarker, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		} else if (col.gameObject.CompareTag(Tags.envdamage)) {
			Destroy(this.gameObject);
		}
	}
}
