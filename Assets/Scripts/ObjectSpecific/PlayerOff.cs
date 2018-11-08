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
		horizontalAxis = PlayerInputTranslator.GetHorizontalAxis(PlayerInputTranslator.Player.OFF);
        verticalAxis = PlayerInputTranslator.GetVerticalAxis(PlayerInputTranslator.Player.OFF);
        jump = PlayerInputTranslator.GetJump(PlayerInputTranslator.Player.OFF);
        interact = PlayerInputTranslator.GetRightInteract(PlayerInputTranslator.Player.OFF);
        reset = PlayerInputTranslator.GetReset(PlayerInputTranslator.Player.OFF);
        pickUp = PlayerInputTranslator.GetPickup(PlayerInputTranslator.Player.OFF);
        base.Start();
    }
}
