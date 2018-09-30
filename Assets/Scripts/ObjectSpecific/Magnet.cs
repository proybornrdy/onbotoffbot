using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public float magneticRange;
    public bool startOn = false;
    public GameObject[] magneticObjects;
    Rigidbody magnetRb;
    bool on = false;

    // Use this for initialization
    void Start () {
        if (startOn)
        {
            TurnOn();
        }
        magnetRb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            foreach (GameObject magneticObject in magneticObjects)
            {
                Magnetic magnetic = magneticObject.GetComponent<Magnetic>();
                Rigidbody magneticRb = magneticObject.GetComponent<Rigidbody>();
                if (!magnetic.GetIsColliding())
                {
                    magnetic.GetPulled();
                }
            }
        }
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
            foreach (GameObject magneticObject in magneticObjects)
            {
                Rigidbody magneticRb = magneticObject.GetComponent<Rigidbody>();
                Magnetic magnetic = magneticObject.GetComponent<Magnetic>();
                print("tag: " + magneticObject.tag);
                if (magneticObject.tag != "Player" && magnetic.InPullingRange(magnetRb, magneticRb, magneticRange))
                {
                    magneticRb.drag = Mathf.Infinity;
                }
            }
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            foreach (GameObject magneticObject in magneticObjects)
            {
                Rigidbody magneticRb = magneticObject.GetComponent<Rigidbody>();
                if (magneticObject.tag != "Player" && magneticRb.drag == Mathf.Infinity)
                {
                    magneticRb.drag = 0;
                }
            }
        }
    }

    public override bool IsOn()
    {
        return on;
    }
}
