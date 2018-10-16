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
	public static float flightDampener = 0.3f;

	// Interactable Objects
	public static GameObject Door;

	// Game State
	static private float time = 0; // time since game began
	static private bool gamePlaying = true; // true: game still going, falst: game over
	static private string reason; // reason game is over if it's over

    public GameObject[] rooms;
    public Door[] doors;
    public bool isTestLevel = true;

    CameraController cc;
    private int currentLevel = 0;

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
        for (int i = 0; i < doors.Length; i++) doors[i].index = i;
        cc = GameObject.Find("CameraController").GetComponent<CameraController>();
    }

    private void Start()
    {
        if (rooms.Length > 0)
        {
            for (int i = 1; i < rooms.Length; i++) rooms[i].SetActive(false);
            rooms[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
	{
		if (gamePlaying)
		{
			time += Time.deltaTime;
            if (rooms.Length !=0) cc.changeCameraPos(rooms[currentLevel]);
		}
	}

    public void DoorOpened(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
            rooms[index + 1].SetActive(true);
    }

    public void DoorClosed(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
            rooms[index + 1].SetActive(false);
    }

    public void PlayersMovedToRoom(int index)
    {
        if (index > 0)
        {
            rooms[index - 1].SetActive(false);
            currentLevel = index;
        }
    }
}
