using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Toggleable
{
	public int extensionRange = 1;
    public Transform arm;
    public Transform moveablePart;
	public bool startOn = false;
    bool on = false;
    bool extended = false;
	bool isColliding = false;

    // Use this for initialization
    void Start () {
        if (startOn) {
			TurnOn();
		}
    }

    // Update is called once per frame
    void Update () {
        isColliding = false;
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
        moveablePart.localPosition += new Vector3(-1f * extensionRange, 0, 0);
        //cylinder.transform.localPosition += new Vector3(-0.5f, 0, 0);
        arm.localScale += new Vector3(4f * extensionRange, 0, 0);
        extended = true;
    }

    public void Contract()
    {
        moveablePart.localPosition -= new Vector3(-1f * extensionRange, 0, 0);
        //cylinder.transform.localPosition += new Vector3(0.5f, 0, 0);
        arm.localScale += new Vector3(-4f * extensionRange, 0, 0);
        extended = false;
    }
	
	void OnTriggerEnter(Collider other){
		if (isColliding == true) return;
		//only push if not extended yet
		if (on) {
			//Make sure objects are pushed along the appropriate axis
            Vector3 moveDirection = Utils.NearestCardinal(other.transform.position - transform.position);
			print(moveDirection);
			other.transform.position += moveDirection * extensionRange;
			isColliding = true;
        }
	}
	
	
	
	void OnTriggerExit(Collider other) {
		;
	}
	
}
