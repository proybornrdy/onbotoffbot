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
		horizontalAxis = PlayerInputTranslator.GetHorizontalAxis(Player.ON);
		verticalAxis = PlayerInputTranslator.GetVerticalAxis(Player.ON);
		jump = PlayerInputTranslator.GetJump(Player.ON);
		interact = PlayerInputTranslator.GetLeftInteract(Player.ON);
        reset = PlayerInputTranslator.GetReset(Player.ON);
        pickUp = PlayerInputTranslator.GetPickup(Player.ON);
        base.Start();
    }
}
