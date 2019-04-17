using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    //World constants
    public static float gravity = 20f;
    // Players
    public static GameObject OnPlayer;
    public static GameObject OffPlayer;
    public float playerMovementSpeed;

    public static bool snapJumpingStatic = false;
    public static float PlayerJumpHeight = 8f;
    public static float flightDampener = 0.2f;
    public bool snapJumping = false;

    void Awake()
    {
        Physics.gravity = new Vector3(0, -Parameters.gravity, 0);
        OnPlayer = GameObject.Find("PlayerOn");
        OffPlayer = GameObject.Find("PlayerOff");
        OnPlayer.GetComponent<PlayerOn>().movementSpeed = playerMovementSpeed;
        OffPlayer.GetComponent<PlayerOff>().movementSpeed = playerMovementSpeed;
        //if (!isTestLevel) for (int i = 0; i < doors.Length; i++) doors[i].index = i;
        snapJumpingStatic = snapJumping;
        Application.targetFrameRate = 300;
    }
}
