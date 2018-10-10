using UnityEngine;

public class LevelController : MonoBehaviour
{

    //World constants
    public static float gravity = 20f;
	// Players
	public static GameObject OnPlayer;
	public static GameObject OffPlayer;
	public static float PlayerMovementSpeed = 5;
	public static float moveSpeed = .05f;
	public static float PlayerJumpHeight = 6f;

	// Interactable Objects
	public static GameObject Door;

	// Game State
	static private float time = 0; // time since game began
	static private bool gamePlaying = true; // true: game still going, falst: game over
	static private string reason; // reason game is over if it's over

	public static bool gameGoing()
	{
		return gamePlaying;
	}

	public static void endGame(string reason)
	{
		gamePlaying = false;
		LevelController.reason = reason;
	}

	public static string getReason()
	{
		return reason;
	}

	public static float getTime()
	{
		return time;
	}

	void Awake()
	{
		OnPlayer = GameObject.Find("PlayerOn");
		OffPlayer = GameObject.Find("PlayerOff");
		Door = GameObject.Find("Door");
	}

	// Update is called once per frame
	void Update()
	{
		if (gamePlaying)
		{
			time += Time.deltaTime;
		}

		// if ((OnPlayerDoor.transform.position - OnPlayer.transform.position).magnitude < (1.5) * Mathf.Sqrt(2) &&
		//	(OffPlayerDoor.transform.position - OffPlayer.transform.position).magnitude < (1.5)*Mathf.Sqrt(2))
		// Debug.Log(OnPlayer.transform.position);
		if (OnPlayer.transform.position.x > 4.4 && OffPlayer.transform.position.x > 4.4)
		{
			LevelController.endGame("Victory");
		}
	}
}
