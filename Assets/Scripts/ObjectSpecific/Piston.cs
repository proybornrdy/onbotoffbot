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
    bool moving = false;
    float t;
    Vector3 initPos;
    Vector3 maxPos;
    float distance;
    int enters = 0;

    // Use this for initialization
    void Start()
    {
        t = 0;
        initPos = moveablePart.position;
        maxPos = initPos + (transform.forward * extensionRange);
        distance = Vector3.Distance(initPos, maxPos);
        if (startOn)
        {
            TurnOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (on && !moving) Extend();
        if (!on && !moving) Retract();

    }

    public override void TurnOn()
    {
        on = true;
        Extend();
        SoundController.instance.playSoundEffect("pistonOn");
    }

    public override void TurnOff()
    {
        on = false;
        Retract();
        SoundController.instance.playSoundEffect("pistonOff");
    }

    public override bool IsOn()
    {
        return on;
    }

    public void Extend()
    {
        StartCoroutine("ExtendCoroutine");
    }

    IEnumerator ExtendCoroutine()
    {
        StopCoroutine("RetractCoroutine");
        moving = true;
        while (t < 1)
        {
            float increment = speed * Time.deltaTime / distance;
            t += increment;
            moveablePart.position = Vector3.Lerp(initPos, maxPos, t);
            arm.localScale += Vector3.right * increment * 4;
            yield return null;
        }
        t = 1;
        moving = false;
    }

    public void Retract()
    {
        StartCoroutine("RetractCoroutine");
    }

    IEnumerator RetractCoroutine()
    {
        StopCoroutine("ExtendCoroutine");
        moving = true;
        while (t > 0)
        {
            float increment = speed * Time.deltaTime / distance;
            t -= increment;
            moveablePart.position = Vector3.Lerp(initPos, maxPos, t);
            arm.localScale -= Vector3.right * increment * 4;
            yield return null;
        }
        moving = false;
        t = 0;
    }
}
