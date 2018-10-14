﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : Toggleable {
    public GameObject child;
    public float speed = 0.5f;
    public float maxOffset = 0;
    float offset = 0;
    int direction = 1;
    Vector3 initialPosition;
    bool on = false;
    public Axis axis = Axis.x;
    bool snapped = true;

    // Use this for initialization
    void Start ()
    {
        initialPosition = child.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            if (offset >= maxOffset) direction = -1;
            else if (offset <= 0) direction = 1;
            offset += speed * direction * Time.deltaTime;
            MoveToNewPosition();
        }
        else if (!snapped)
        {
            offset = Mathf.Round(offset);
            MoveToNewPosition();
            snapped = true;
        }
    }

    void MoveToNewPosition()
    {
        Vector3 pos = initialPosition + ((axis == Axis.x ? Vector3.right : axis == Axis.z ? Vector3.back : Vector3.down) * offset);
        child.transform.localPosition = pos;
    }

    public override bool IsOn()
    {
        return on;
    }

    public override void TurnOff()
    {
        on = false;
    }

    public override void TurnOn()
    {
        on = true;
        snapped = false;
    }
}