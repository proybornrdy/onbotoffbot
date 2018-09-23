using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Toggleable
{
    public float pauseTime = 2f;

    private float nextPauseAngle = 180f;
    private float rotated = 0f;
    private bool pause = false;
    private float originPosY, maxPosY;
    private Vector3 movement;

    private bool on;
    private bool startOn = false;

    void Start()
    {
        originPosY = transform.position.y;
        maxPosY = originPosY + 2;
        movement = Vector3.up;
        on = false;

        if (startOn)
        {
            TurnOn();
        }
        
    }

    public override void TurnOn()
    {
        on = true;
    }
    public override void TurnOff()
    {
        on = false;
    }

    void Update()
    {
        if (on)
        {
            if (!pause)
            {
                float r = 45f * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, r));
                rotated += Mathf.Abs(r);

                if (rotated >= nextPauseAngle)
                {
                    //pause rotation
                    pause = true;
                    nextPauseAngle += 180f;
                }
            }
            else
            {

                transform.position = transform.position + (movement * Time.deltaTime);
                if (transform.position.y >= maxPosY)
                {
                    movement = Vector3.down;
                    //unpause
                    pause = false;
                }
                else if (transform.position.y <= originPosY)
                {
                    movement = Vector3.up;
                    pause = false;
                }
            }
        }
        
    }
}
