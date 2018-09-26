﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour {

	public GameObject onPlayer;
    public Toggleable toggleable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!LevelController.gameGoing())
		{
			return;
		}
		Vector3 onPlayerPos = onPlayer.transform.position;
		Vector3 buttonPos = transform.position;
        if (Input.GetButton("Fire1") && inRange(onPlayerPos, buttonPos))
        {
            toggleable.TurnOn();
        }
    }

    bool inRange(Vector3 onPlayerPos, Vector3 buttonPos)
    {
        return System.Math.Pow(onPlayerPos.x - buttonPos.x, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.y - buttonPos.y, 2) <= 1 &&
            System.Math.Pow(onPlayerPos.z - buttonPos.z, 2) <= 1;
    }
}
