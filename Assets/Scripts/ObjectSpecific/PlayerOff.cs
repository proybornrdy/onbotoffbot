using UnityEngine;

public class PlayerOff : PlayerBase
{
    new void Start ()
    {
        rb = GetComponent<Rigidbody>();
		/*horizontalAxis = "POffHorizontal";
        verticalAxis = "POffVertical";
        jump = "POffJump";
        interact = "Button Off";*/
		horizontalAxis = PlayerInputTranslator.GetHorizontalAxis(Player.OFF);
        verticalAxis = PlayerInputTranslator.GetVerticalAxis(Player.OFF);
        jump = PlayerInputTranslator.GetJump(Player.OFF);
        interact = PlayerInputTranslator.GetRightInteract(Player.OFF);
        reset = PlayerInputTranslator.GetReset(Player.OFF);
        pickUp = PlayerInputTranslator.GetPickup(Player.OFF);
        base.Start();
    }
}
