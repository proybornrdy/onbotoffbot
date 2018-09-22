using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Toggleable
{

    Vector3 movement;
    Rigidbody pistonRigidbody;
    public GameObject cylinder;
    bool extended = false;
    
    bool on;

    // Use this for initialization
    void Start () {
        pistonRigidbody = GetComponent<Rigidbody>();
        on = false;
    }

    // Update is called once per frame
    void Update () {
        
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
            Extend();
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            Contract();
        }
    }

    public void Extend()
    {
        transform.localPosition += new Vector3(-1f, 0, 0);
        //cylinder.transform.localPosition += new Vector3(-0.5f, 0, 0);
        cylinder.transform.localScale += new Vector3(4f, 0, 0);
        extended = true;
    }

    public void Contract()
    {
        transform.localPosition -= new Vector3(-1f, 0, 0);
        //cylinder.transform.localPosition += new Vector3(0.5f, 0, 0);
        cylinder.transform.localScale += new Vector3(-4f, 0, 0);
        extended = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (extended)
        {
            float newX = (float)Math.Round((pistonRigidbody.position.x - 1) * 2, MidpointRounding.AwayFromZero) / 2;
            other.transform.localPosition = new Vector3(newX, other.transform.localPosition.y, other.transform.localPosition.z);
        }
    }


    void OnTriggerExit(Collider other)
    {
        
    }
}
