﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	public float interval = .2f;
	public bool randomStart;

	public GameObject toSpawn;

	void Start() {
		StartCoroutine(Main());
	}

	IEnumerator Main() {
		if (randomStart) {
			yield return new WaitForSeconds(Random.Range(0, 2));
		}
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		yield return new WaitForSeconds(interval);
		Instantiate(toSpawn, this.transform.position, Quaternion.identity);
		StartCoroutine(Spawn());
	}
}
