using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelController : MonoBehaviour
{

    //World constants
    public static float gravity = 20f;
    // Players
    public static GameObject OnPlayer;
    public static GameObject OffPlayer;
    public float playerMovementSpeed;

    public static bool snapJumpingStatic = false;
    public static float PlayerJumpHeight = 8f;
    public static float flightDampener = 0.3f;
    public bool snapJumping = false;

    // Interactable Objects
    public static GameObject Door;

    // Game State
    static private float time = 0; // time since game began
    static private bool gamePlaying = true; // true: game still going, falst: game over
    static private string reason; // reason game is over if it's over

    public RoomArray2D[] rooms;
	public GameObject[] backtrackBlockers;
	public Door[] doors;
    public bool isTestLevel = true;

    CameraController cc;
    private int currentRoom = 0;
    private int newRoom;
    private bool roomFadeIn = false;
    private bool roomFadeOut = false;

	public static bool InMenue = false;
	private GameObject PauseSceneRoot;

	private string[] LevelProgresion = {
        "Assets/Scenes/Progression chunks/Section 1.unity",
        "Assets/Scenes/Progression chunks/Section 2.unity",
        //"Assets/Scenes/Level Ideas/BasicPistonPuzzle.unity",
       //"Assets/Scenes/Level Ideas/PressurePlateLevel.unity",
		//"Assets/Scenes/Level Ideas/2-6.unity",
		"Assets/Scenes/IntoScene.unity"
    };

    public string[] RoomScenes;
    public string[] RoomNames;

    delegate void RoomAction();

    TutorialStateMachine tutorialStateMachine;
    HUDController hudController;
    List<RoomAction> roomActions;

	private int oldTime = 0;
	private static GameStateLog gameStateLog;

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

    void SetUpRoomActions()
    {
        roomActions = new List<RoomAction>();
        //room 1.1
        roomActions.Add(delegate ()
        {
            ShowTutorial(TutorialState.Jump);
        });
        //room 1.2
        roomActions.Add(delegate ()
        {
            ShowTutorial(TutorialState.InteractOn);
        });
        //room 1.3
        roomActions.Add(delegate ()
        {
            ShowTutorial(TutorialState.InteractOff);
        });
        //room 1.4
        roomActions.Add(delegate ()
        {
            ;
        });
        //room 1.5
        roomActions.Add(delegate ()
        {
            ;
        });
        //room 2.1
        roomActions.Add(delegate ()
        {
            ShowTutorial(TutorialState.PickUpItem);
        });
    }

    void ShowTutorial(TutorialState state)
    {
        if (!tutorialStateMachine.GetStateValue(state))
        {
            tutorialStateMachine.SetStateValue(state, true);
            hudController.ReceiveText(tutorialStateMachine.GetStateText(state));
            hudController.ReceiveButtons(tutorialStateMachine.GetStateButtons(state));
        }
    }

    void Awake()
    {
        OnPlayer = GameObject.Find("PlayerOn");
        OffPlayer = GameObject.Find("PlayerOff");
        OnPlayer.GetComponent<PlayerOn>().movementSpeed = playerMovementSpeed;
        OffPlayer.GetComponent<PlayerOff>().movementSpeed = playerMovementSpeed;
        //if (!isTestLevel) for (int i = 0; i < doors.Length; i++) doors[i].index = i;
        cc = GameObject.Find("CameraController").GetComponent<CameraController>();
        snapJumpingStatic = snapJumping;

		gameStateLog = new GameStateLog(SceneManager.GetActiveScene().name);
        tutorialStateMachine = FindObjectOfType<TutorialStateMachine>();
        hudController = FindObjectOfType<HUDController>();
    }

    private void Start()
    {
        Physics.gravity = new Vector3(0, -LevelController.gravity, 0);
        SetUpRoomActions();
        if (!isTestLevel && rooms.Length > 0)
        {
            for (int i = 1; i < rooms.Length; i++)
                for (int j = 0; j < rooms[i].Length; j++) rooms[i][j].SetActive(false);

            for (int j = 0; j < rooms[0].Length; j++)
            {
                rooms[0][j].SetActive(true);
            }
        }


        for (int i = 0; i < doors.Length; i++) doors[i].index = i;

		PauseSceneRoot = GameObject.FindWithTag("PauseSceenRoot");
		if (!PauseSceneRoot)
		{
			Debug.Log("Not Found");
			// SceneManager.LoadScene("InGameMenue", LoadSceneMode.Additive);
		}
        currentRoom = 0;
        if (!isTestLevel && rooms.Length != 0) cc.changeCameraPos(rooms[currentRoom][0]);
        if (!isTestLevel )PlayersMovedToRoom(0);
    }

    public static void ToggleMenue()
    {
		InMenue = !InMenue;
	}

	// Update is called once per frame
	void Update()
	{
		if (!PauseSceneRoot)
		{
			PauseSceneRoot = GameObject.FindWithTag("PauseSceenRoot");
			if (PauseSceneRoot)
			{
				PauseSceneRoot.SetActive(false);
			}
		}
		if (gamePlaying)
		{
			time += Time.deltaTime;
            //if (!isTestLevel && rooms.Length != 0) cc.zoomCamera(OnPlayer, OffPlayer); 
            

			// log postion every second
			if (((int)time) != oldTime)
			{
				gameStateLog.LogPositions(OnPlayer.transform.position, OffPlayer.transform.position);
				oldTime = (int)time;
			}

        }
        if (!isTestLevel) cc.changeCameraPos(rooms[currentRoom][0]);

        Time.timeScale = (InMenue) ? 0.00f : 1.00f;
        if (PauseSceneRoot)
        {
            PauseSceneRoot.SetActive(InMenue);
        }
	}

    public void DoorOpened(int index)
    {
        print("here");
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
        {
            newRoom = index + 1;
            rooms[newRoom][0].HideBlockingWalls();
            for (int j = 0; j < rooms[newRoom].Length; j++)
            {
				rooms[newRoom][j].SetActive(true);

                //rooms[index + 1][j].transform.Find("Walls").gameObject.SetActive(false);

                /*since all rooms are just activated from deactivation, 
                it needs to be invisible first in order for it to be faded in*/
                rooms[newRoom][j].FadeIn();
            }
        }

    }

    public void DoorClosed(int index)
    {
        //if (!isTestLevel && index != -1 && index < rooms.Length - 1)
        //    for (int j = 0; j < rooms[index + 1].Length; j++)
        //        StartCoroutine(RoomFade(rooms[index + 1][j], true));
    }

    public void PlayersMovedToRoom(int index)
    {
        if (index != -1)
        {
            print(index);
            currentRoom = index;
            StartCoroutine("RoomFadeDelay", index);
            roomActions[index]();
        }
    }

    IEnumerator RoomFadeDelay(int index)
    {
        yield return new WaitForSeconds(0.5f);


        if (!isTestLevel && index > 0 && index <= rooms.Length - 1)
        {
            if (index == 6)
            {
                int nextSceneIndex = Array.IndexOf(LevelProgresion, SceneManager.GetActiveScene().path);
                //save the log, and move on to next level
                gameStateLog.SaveGameStateLog();
                SceneManager.LoadScene(LevelProgresion[nextSceneIndex + 1]);
            }

            for (int j = 0; j < rooms[index - 1].Length; j++)
            {
                rooms[index - 1][j].FadeOut();
            }
            backtrackBlockers[index - 1].SetActive(true);
        }
    }

    public static void ResetScene()
    {
		gameStateLog.SaveGameStateLog();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		time = 0;

    }
}
