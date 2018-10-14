﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public float maxRange = 5f; // maximum distance the magnet will begin pulling an object from
    public float maxStrength = 100f; // Maximum strength the magnet will pull something right next to it. Goes down as the object gets further away

    public bool startOn = false;
    public GameObject[] magneticObjects;
    bool on = false;

    // Use this for initialization
    void Start () {
        if (startOn)
        {
            TurnOn();            
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        if (on)
        {
            foreach (GameObject magneticObject in magneticObjects)
            {
                Magnetic magnetic = magneticObject.GetComponent<Magnetic>();
                magnetic.GetPulled();
            }
        }
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
            print("Magnet is ON");
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            print("Magnet is OFF");
            foreach (GameObject magneticObject in magneticObjects)
            {
                Rigidbody magneticRb = magneticObject.GetComponent<Rigidbody>();
                if (magneticObject.tag != "Player" && magneticRb.drag == Mathf.Infinity)
                {
                    magneticRb.drag = 0;
                    magneticObject.transform.parent = null;
                }
                magneticObject.GetComponent<Magnetic>().SetIsColliding(false);
            }
        }
    }

    public override bool IsOn()
    {
        return on;
    }
}
