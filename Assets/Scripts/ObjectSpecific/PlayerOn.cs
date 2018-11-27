using UnityEngine;

public class PlayerOn : PlayerBase
{
    public string playerCurrentRoom;
    public string playerRoomCheck;
    new void Start ()
    {
		rb = GetComponent<Rigidbody>();
        playerCurrentRoom = "";
        playerRoomCheck = "";
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
