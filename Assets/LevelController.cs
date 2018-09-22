using UnityEngine;

public class LevelController : MonoBehaviour
{

    //World constants
    public static float gravity = 20f;
	// Players
	static GameObject OnPlayer;
	static GameObject OffPlayer;
	public static float PlayerMovementSpeed = 5;
	public static float PlayerJumpHeight = 7;

	// Interactable Objects
	static GameObject P1Door;
	static GameObject P2Door;
	static GameObject[] Pistons;

	// Game State
	static private float time; // true: game still going, falst: game over
	static private bool gamePlaying; // true: game still going, falst: game over
	static private string reason; // reason game is over if it's over

	public static bool getGameStatus()
	{
		return gamePlaying;
	}

	public static void endGame(string reason)
	{
		gamePlaying = false;
		LevelController.reason = reason;
	}

	public static float getTimePlayed()
	{
		return time;
	}

	// Update is called once per frame
	void Update()
	{
		if (gamePlaying)
		{
			time += Time.deltaTime;
		}

		// if (P1Door.transform.position - OnPlayer)
	}
}
