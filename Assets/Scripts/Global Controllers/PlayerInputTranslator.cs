using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTranslator : MonoBehaviour {
	public enum Player
	{
		ON, OFF
	}

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
		if (p == P1)
		{
			return "P1Horizontal";
		}
		else
		{
			return "P2Horizontal";
		}
	}

	public static string GetVerticalAxis(Player p)
	{
		if (p == P1)
		{
			return "P1Vertical";
		}
		else
		{
			return "P2Vertical";
		}
	}

	public static string GetLeftInteract(Player p)
	{
		if (p == P1)
		{
			return "P1LeftInteract";
		}
		else
		{
			return "P2LeftInteract";
		}
	}

	public static string GetRightInteract(Player p)
	{
		if (p == P1)
		{
			return "P1RightInteract";
		}
		else
		{
			return "P2RightInteract";
		}
	}

	public static string GetJump(Player p)
	{
		if (p == P1)
		{
			return "P1Jump";
		}
		else
		{
			return "P2Jump";
		}
	}
}