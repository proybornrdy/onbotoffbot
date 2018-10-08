using UnityEngine;

public class PlayerOn : PlayerBase
{
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        horizontalAxis = "POnHorizontal";
        verticalAxis = "POnVertical";
        jump = "POnJump";
        interact = "Button On";
    }
}
