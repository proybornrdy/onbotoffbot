using UnityEngine;

public class PlayerOn : PlayerBase
{
    new void Start ()
    {
		rb = GetComponent<Rigidbody>();
        /*horizontalAxis = "POnHorizontal";
        verticalAxis = "POnVertical";
        jump = "POnJump";
        interact = "Button On";*/
		horizontalAxis = PlayerInputTranslator.GetHorizontalAxis(PlayerInputTranslator.Player.ON);
		verticalAxis = PlayerInputTranslator.GetVerticalAxis(PlayerInputTranslator.Player.ON);
		jump = PlayerInputTranslator.GetJump(PlayerInputTranslator.Player.ON);
		interact = PlayerInputTranslator.GetLeftInteract(PlayerInputTranslator.Player.ON);
        reset = PlayerInputTranslator.GetReset(PlayerInputTranslator.Player.ON);
        pickUp = PlayerInputTranslator.GetPickup(PlayerInputTranslator.Player.ON);
        base.Start();
    }
}
