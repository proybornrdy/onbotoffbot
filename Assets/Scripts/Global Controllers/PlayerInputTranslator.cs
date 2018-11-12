using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTranslator : MonoBehaviour {

	private static Player P1 = Player.ON;
	private static Player P2 = Player.OFF;
	// Use this for initialization
	public static void SetP1P2(Player p1, Player p2)
	{
		P1 = p1;
		P2 = p2;
	}

	public static string GetHorizontalAxis(Player p)
    {
        return p == P1 ? "P1Horizontal" : "P2Horizontal";
	}

	public static string GetVerticalAxis(Player p)
    {
        return p == P1 ? "P1Vertical" : "P2Vertical";
	}

	public static string GetLeftInteract(Player p)
    {
        return p == P1 ? "P1LeftInteract" : "P2LeftInteract";
	}

	public static string GetRightInteract(Player p)
    {
        return p == P1 ? "P1RightInteract" : "P2RightInteract";
    }

    public static string GetPickup(Player p)
    {
        return p == P1 ? "P1Pickup" : "P2Pickup";
    }

    public static string GetJump(Player p)
	{
        return p == P1 ? "P1Jump" : "P2Jump";
	}

    public static string GetReset(Player p)
    {
        return p == P1 ? "P1Reset" : "P2Reset";
    }

    public static string GetMenu(Player p)
    {
        return p == P1 ? "P1Menu" : "P2Menu";
    }
}