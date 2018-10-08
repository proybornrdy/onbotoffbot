using UnityEngine;

public class PlayerOff : PlayerBase
{
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        horizontalAxis = "POffHorizontal";
        verticalAxis = "POffVertical";
        jump = "POffJump";
        interact = "Button Off";
    }
}
