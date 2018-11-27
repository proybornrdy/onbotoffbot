using UnityEngine;

public class PlayerOff : PlayerBase
{
    public string playerCurrentRoom;
    public string playerRoomCheck;
    new void Start ()
    {
        rb = GetComponent<Rigidbody>();
        playerCurrentRoom = "";
        playerRoomCheck = "";
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

        //animator.SetBool("PlayerOff", true);
    }
}
