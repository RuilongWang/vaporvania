﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable {

	public Sprite portrait;
	public string text;

	public string signName = null;

	UIController uc;

	void Start() {
		uc = GameObject.Find("GameController").GetComponent<UIController>();
	}

	public override void Interact(GameObject player) {
		uc.OpenDialogue(this);

		uc.RenderText(text);
	}
}
