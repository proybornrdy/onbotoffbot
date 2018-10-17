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
    public static bool snapJumping = false;
	public static float PlayerJumpHeight = 6f;
	public static float flightDampener = 0.3f;

	// Interactable Objects
	public static GameObject Door;

	// Game State
	static private float time = 0; // time since game began
	static private bool gamePlaying = true; // true: game still going, falst: game over
	static private string reason; // reason game is over if it's over

    public MultiDimensionalGameObject[] rooms;
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
            for (int i = 1; i < rooms.Length; i++)
                for (int j = 0; j < rooms[i].Length; j++) rooms[i][j].SetActive(false);

            for (int j = 0; j < rooms[0].Length; j++) rooms[0][j].SetActive(true);
        }
        var jumpPoints = TagCatalogue.FindAllWithTag(Tag.JumpPoint);
        foreach (var j in jumpPoints) j.gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
	{
		if (gamePlaying)
		{
			time += Time.deltaTime;
            if (rooms.Length !=0) cc.changeCameraPos(rooms[currentLevel][0]);
		}
	}

    public void DoorOpened(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
            for (int j = 0; j < rooms[index + 1].Length; j++)
                rooms[index + 1][j].SetActive(true);
    }

    public void DoorClosed(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
            for (int j = 0; j < rooms[index + 1].Length; j++)
                rooms[index + 1][j].SetActive(false);
    }

    public void PlayersMovedToRoom(int index)
    {
        if (index > 0)
        {
            for (int j = 0; j < rooms[index - 1].Length; j++)
                rooms[index - 1][j].SetActive(false);
            currentLevel = index;
        }
    }
}

[System.Serializable]
public class MultiDimensionalGameObject
{
    public GameObject[] arr;
    public GameObject this[int i]
    {
        get
        {
            return arr[i];
        }
        set
        {
            arr[i] = value;
        }
    }
    public int Length
    {
        get { return arr.Length; }
    }
}
