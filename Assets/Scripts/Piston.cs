using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Toggleable
{
    public float extensionRange = 0.9f;
    public float speed = 0.1f;
    public Transform arm;
    public Transform moveablePart;
    public bool startOn = false;
    bool on = false;
    float initMoveablePosition;
    float initExtension;
    float endMoveablePosition;
    float endExtension;

    // Use this for initialization
    void Start()
    {
        if (startOn)
        {
            TurnOn();
        }
        initMoveablePosition = moveablePart.localPosition.x;
        initExtension = arm.localScale.x;
        endMoveablePosition = moveablePart.localPosition.x + (-extensionRange);
        endExtension = arm.localScale.x + 4f * extensionRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
        
        if(on)
        {
            Extend();
        }
        else
        {
            Contract();
        }
        
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
        }
    }

    public void Extend()
    {
        if (arm.localScale.x < endExtension && moveablePart.localPosition.x > endMoveablePosition)
        {
            arm.localScale += new Vector3(speed, 0, 0);
            moveablePart.localPosition -= new Vector3((speed/4), 0, 0);
        }
    }

    public void Contract()
    {        
        if (arm.localScale.x > initExtension && moveablePart.localPosition.x < initMoveablePosition)
        {
            arm.localScale -= new Vector3(speed, 0, 0);
            moveablePart.localPosition += new Vector3((speed/4), 0, 0);
        }
    }
    
    void OnCollisionStay(Collision other)
    {
        if (on && other.gameObject.tag == "MoveableBlock")
        {
            Vector3 moveDirection = Utils.NearestCardinal(other.transform.position - transform.position);
            other.transform.position += moveDirection * speed;
        }
    }    
}
