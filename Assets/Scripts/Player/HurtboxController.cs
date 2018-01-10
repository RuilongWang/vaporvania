﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxController : MonoBehaviour {
	public int damage = 1;
	public float hitstop = .04f;
	public bool cameraShake = false;
	public Vector2 knockbackVector = new Vector2(3, 1);
	public GameObject hitmarker;

	PlayerController pc;

	void Start() {
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	public int GetDamage() {
		if (this.tag == Tags.playerAttack) {
			return this.damage * pc.BASE_ATTACK_DMG;
		} else {
			return this.damage;
		}
	}
}
