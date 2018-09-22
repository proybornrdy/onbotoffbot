using UnityEngine;

public class LevelController : MonoBehaviour
{

	// Players
	static GameObject OnPlayer;
	static GameObject OffPlayer;
	public static float PlayerMovementSpeed = 10;
	public static float PlayerJumpHeight = 10;

	// Interactable Objects
	static GameObject Door;
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
	}
}
