using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LevelController : MonoBehaviour
{

    //World constants
    public static float gravity = 20f;
    // Players
    public static GameObject OnPlayer;
    public static GameObject OffPlayer;
    public static float PlayerMovementSpeed = 5;
    public static float moveSpeed = .05f;
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

    public MultiDimensionalGameObject[] rooms;
	public GameObject[] backtrackBlockers;
	public Door[] doors;
    public bool isTestLevel = true;

    CameraController cc;
    private int currentRoom = 0;
    private int newRoom;
    private bool roomFadeIn = false;
    private bool roomFadeOut = false;

	private string[] LevelProgresion = {
		"Assets/Scenes/Progression chunks/Section 1.unity",
		"Assets/Scenes/Level Ideas/PressurePlateLevel.unity",
		"Assets/Scenes/Level Ideas/2-6.unity",
		"Assets/Scenes/Level Ideas/BasicPistonPuzzle.unity",
		"Assets/Scenes/IntoScene.unity"
	};

	private int oldTime = 0;
	private GameStateLog gameStateLog;

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
        snapJumpingStatic = snapJumping;

		gameStateLog = new GameStateLog(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        if (rooms.Length > 0)
        {
            for (int i = 1; i < rooms.Length; i++)
                for (int j = 0; j < rooms[i].Length; j++) rooms[i][j].SetActive(false);

            for (int j = 0; j < rooms[0].Length; j++)
            {
                rooms[0][j].SetActive(true);
            }
        }
        var jumpPoints = TagCatalogue.FindAllWithTag(Tag.JumpPoint);
        foreach (var j in jumpPoints) j.gameObject.GetComponent<Renderer>().enabled = false;
        var colliders = TagCatalogue.FindAllWithTag(Tag.Collider);
        foreach (var j in colliders) j.gameObject.GetComponent<Renderer>().enabled = false;
    }

    public static void GoToMenu()
    {
        SceneManager.LoadScene("IntoScene");
    }

    // Update is called once per frame
    void Update()

	{
		if (gamePlaying)
		{
			time += Time.deltaTime;
            if (!isTestLevel && rooms.Length !=0) cc.changeCameraPos(rooms[currentRoom][0]);

			// log postion every second
			if (((int)time) != oldTime)
			{
				gameStateLog.LogPositions(OnPlayer.transform.position, OffPlayer.transform.position);
				oldTime = (int)time;
			}
            
		}
	}

    public void DoorOpened(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
        {
			for (int j = 0; j < rooms[index + 1].Length; j++)
            {
				rooms[index + 1][j].SetActive(true);

				/*since all rooms are just activated from deactivation, 
                it needs to be invisible first in order for it to be faded in*/
				setRoomInvisible(rooms[index + 1][j]);
                StartCoroutine(RoomFade(rooms[index + 1][j], false));
            }
            newRoom = index + 1;
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
        currentRoom = index;
        if (index == rooms.Length - 2)
        {
            // left the last room, are now in the final room
            index = Array.IndexOf(LevelProgresion, SceneManager.GetActiveScene().path);
            SceneManager.LoadSceneAsync(LevelProgresion[index + 1]);
        }
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
        {
            for (int j = 0; j < rooms[index - 1].Length; j++)
            {
                StartCoroutine(RoomFade(rooms[index - 1][j], true));
            }
            backtrackBlockers[index - 1].SetActive(true);
        }
    }

    public static void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        time = 0;

    }

    IEnumerator RoomFade(GameObject room, bool isFading)
    {
        /*Fade out : targetAlpha=0 < currentAlpha=1 (currentAlpha --0.1f)
        Fade in :  currentALpha=0 < targetAlpha=1 (currentAlpha ++0.1f)*/
        Renderer[] rends = room.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < 100; i++)
        {

            foreach (Renderer r in rends)
            {
                changeMaterialModeToFadeMode(r);
                Color alpha = r.material.color;
                if (isFading)
                {
                    if (alpha.a > 0f) alpha.a -= 0.01f;
                    else alpha.a = 0.0f;
                }
                else
                {
                    if (alpha.a < 1)
                        alpha.a += 0.01f;
                }                
                r.material.color = alpha;            

            }
            yield return null;
        }

        if (!isFading) /*since the faded out room needs to stay invisible require it to stay in Fade mode. So this only applies to room that is being faded in*/
        {
            foreach (Renderer r in rends) changeMaterialModeToOpaqueMode(r);            
        }
    }


    private void setRoomInvisible(GameObject room)
    {
        Renderer[] rends = room.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            changeMaterialModeToFadeMode(r);
            Color alpha = r.material.color;
            alpha.a = 0.2f;
            r.material.color = alpha;
        }
    }

   

    private void changeMaterialModeToFadeMode(Renderer rd)
    {

        rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        rd.material.SetInt("_ZWrite", 0);
        rd.material.EnableKeyword("_ALPHABLEND_ON");
    }

    private void changeMaterialModeToOpaqueMode(Renderer rd)
    {
        rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        rd.material.SetInt("_ZWrite", 1);
        rd.material.DisableKeyword("_ALPHABLEND_ON");
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
