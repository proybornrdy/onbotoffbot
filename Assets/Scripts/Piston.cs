using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Toggleable
{
    Vector3 movement;
	public int extensionRange = 1;
    public bool extended = false;
    public Transform arm;
    public Transform moveablePart;
    bool on;
	bool isColliding = false;

    // Use this for initialization
    void Start () {
        on = false;
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
		
            Vector3 moveDirection = (other.transform.position - transform.position).normalized;
			print(moveDirection);
			other.transform.position += moveDirection * extensionRange;
			isColliding = true;
        
	}
	
	void OnTriggerExit(Collider other) {
		;
	}
	
}
