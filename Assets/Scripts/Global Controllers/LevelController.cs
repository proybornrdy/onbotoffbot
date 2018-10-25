using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Door[] doors;
    public bool isTestLevel = true;

    CameraController cc;
    private int currentRoom = 0;
    private int newRoom;
    private bool roomFadeIn = false;
    private bool roomFadeOut = false;

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

            for (int j = 0; j < rooms[0].Length; j++) rooms[0][j].SetActive(true);
        }
        var jumpPoints = TagCatalogue.FindAllWithTag(Tag.JumpPoint);
        foreach (var j in jumpPoints) j.gameObject.GetComponent<Renderer>().enabled = false;
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
            //if (roomFadeIn)
            //{
            //    for (int j = 0; j < rooms[newRoom].Length; j++)
            //        fadeInRoom(rooms[newRoom][j]);
            //}
            //if (roomFadeOut)
            //{
            //    for (int j = 0; j < rooms[newRoom-1].Length; j++)
            //        fadeOutRoom(rooms[newRoom-1][j]);
            //}
		}
	}

    public void DoorOpened(int index)
    {
        if (!isTestLevel && index != -1 && index < rooms.Length - 1)
        {
            for (int j = 0; j < rooms[index + 1].Length; j++)
            {
                rooms[index + 1][j].SetActive(true);
                //setRoomInvisible(rooms[index + 1][j]);
            }
                
            //roomFadeIn = true;
            //newRoom = index + 1;
        }

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
            {
                //roomFadeOut = true;
                //fadeOutRoom(rooms[index - 1][j]);
                //if (!roomFadeOut)
                    rooms[index - 1][j].SetActive(false);
            }
                
            currentRoom = index;

        }
    }

    public static void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        time = 0;

    }

    //private void setRoomInvisible(GameObject room)
    //{
    //    Renderer[] rends = room.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer r in rends)
    //    {
    //        changeMaterialModeToFadeMode(r);
    //        Color alpha = r.material.color;
    //        alpha.a = 0f;
    //        r.material.color = alpha;
    //    }
    //}

    //private void fadeInRoom(GameObject room)
    //{
    //    Renderer[] rends = room.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer r in rends)
    //    {
    //        Color meshColor = r.material.color;

    //        //Set Alpha
    //        const float alpha = 1f;
    //        meshColor.a = alpha;

    //        r.material.color = Color.Lerp(r.material.color,meshColor,0.1f);
    //        if (Mathf.Approximately(r.material.color.a, 1f))
    //        {
    //            changeMaterialModeToOpaqueMode(r);
    //            roomFadeIn = false;
    //        }
    //    }
    //}

    //private void fadeOutRoom(GameObject room)
    //{
    //    Renderer[] rends = room.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer r in rends)
    //    {
    //        changeMaterialModeToFadeMode(r);
    //        Color meshColor = r.material.color;

    //        //Set Alpha
    //        const float alpha = 0f;
    //        meshColor.a = alpha;

    //        r.material.color = Color.Lerp(r.material.color, meshColor, 0.1f);
    //        if (Mathf.Approximately(r.material.color.a, 0f))
    //        {
    //            changeMaterialModeToOpaqueMode(r);
    //            roomFadeOut = false;
    //        }

    //    }
            
    //}

    //private void changeMaterialModeToFadeMode(Renderer rd)
    //{

    //    rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //    rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //    rd.material.SetInt("_ZWrite", 0);
    //    rd.material.DisableKeyword("_ALPHATEST_ON");
    //    rd.material.EnableKeyword("_ALPHABLEND_ON");
    //    rd.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //    rd.material.renderQueue = 3000;
    //}

    //private void changeMaterialModeToOpaqueMode(Renderer rd)
    //{
    //    rd.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //    rd.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    //    rd.material.SetInt("_ZWrite", 1);
    //    rd.material.DisableKeyword("_ALPHATEST_ON");
    //    rd.material.DisableKeyword("_ALPHABLEND_ON");
    //    rd.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //    rd.material.renderQueue = 3000;
    //}
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
