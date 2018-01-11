﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyOfTheLake : Boss {

    int eyeCount;
    public GameObject eyeContainer;

    //this needs to be a broken prefab since it'll have an animation controller attached to it
    //or just nest it in an empty gameobject, actually, and then change its animator
    public GameObject tiledSludgeContainer;

    //for the sludge throwing attack
    public GameObject thrownSludge;
    //initial vector for throwing sludges, the y value will decrease and she'll throw three at the player
    public Vector2 sludgeVector;
    public Transform sludgePoint;

    public override void Initialize() {
        monologue = new List<DialogueLine>();
        AddLines();
        eyeCount = eyeContainer.transform.childCount;
        uc = GameObject.Find("GameController").GetComponent<UIController>();
        gc = uc.GetComponent<GameController>();
    }


    public override void BossMove() {
        //maybe check for one of a few actions to take?
        if (!fighting || !moving) return;
        ThrowSludges();
    }

    //move back and throw three sludges at the player
    public void ThrowSludges() {
        moving = false;
        anim.SetTrigger("throwSludges");
        //and then set moving to true at the end of the sludge throwing animation
    }

    void MeleeAttack() {
        //wind up and make an attack in the area around herself
    }

    void FloodStage() {
        //disperse into sludge and flood the bottom of the stage
        moving = false;
        StartCoroutine(WaitAndFloodStage(5f));
        StartCoroutine(WaitAndRise(9f));
    }

    IEnumerator WaitAndFloodStage(float seconds) {
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("descend");
        tiledSludgeContainer.GetComponent<Animator>().SetTrigger("rise");
    }

    IEnumerator WaitAndRise(float seconds) {
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("awake");
        StartCoroutine(WaitAndStartMoving(2f));
    }

    IEnumerator WaitAndStartMoving(float seconds) {
        yield return new WaitForSeconds(seconds);
        fighting = true;
        anim.SetBool("fighting", true);
        moving = true;
    }

    void SwitchSides() {
        //move to the other side of the stage
    }

    public override void StartFight() {
        //disable further player interactions
        GetComponent<BoxCollider2D>().enabled = false;
        if (!foughtBefore) {
            Intro();
        } else {
            fighting = true;
            anim.SetBool("fighting", true);
        }
    }

    void Intro() {
        this.foughtBefore = true;
        anim.SetTrigger("awake");
        uc.OpenDialogue(this);
        uc.RenderDialogue(monologue[0]);
    }

    void AddLines() {
        monologue.Add(new DialogueLine(
            "Half sludge, half light...",
            this.bossName,
            0
        ));
        monologue.Add(new DialogueLine(
            "The enviroment does terrible things to one down here.",
            this.bossName,
            0
        ));
        monologue.Add(new DialogueLine(
            "But not to you, yet.\n",
            this.bossName,
            0
        ));
        monologue.Add(new DialogueLine(
            "Are you here to kill? To maim? To take what you can from these wastes?",
            this.bossName,
            0
        ));
        monologue.Add(new DialogueLine(
            "...",
            "VAL",
            -1
        ));
        monologue.Add(new DialogueLine(
            "Get it over with, then.",
            this.bossName,
            0
        ));
    }

    public override void StopTalking() {
        StartCoroutine(WaitAndStartMoving(2f));
    }

    void CloseEye(int eyeNum) {
        GameObject e = eyeContainer.transform.GetChild(eyeCount).gameObject;
        e.GetComponent<BoxCollider2D>().enabled = false;
        e.GetComponent<Animator>().SetTrigger("close");
    }

    public override void OnDamage() {
        //if the current health is below a certain fraction of its total and more than enough eyes are open, close one
        
    }

    public void ThrowSludgeball(int sludgeNum) {
        print("threw a sludge");
        GameObject sludge = (GameObject) Instantiate(thrownSludge, sludgePoint.position, Quaternion.identity);
        float xVec = sludgeVector.x;
        float yVec = sludgeVector.y;
        sludge.GetComponent<Rigidbody2D>().velocity = new Vector2(-xVec, yVec / sludgeNum);
        }
     
}
