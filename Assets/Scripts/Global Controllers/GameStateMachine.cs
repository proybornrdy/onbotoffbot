using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine {
    private static GameStateMachine instance = null;

    public static GameStateMachine Instance
    {
        get
        {
            if (instance == null) instance = new GameStateMachine();
            return instance;
        }
    }

    public bool CompletedRoom_11 { get; set; }

    public bool CompletedRoom_12 { get; set; }

    public bool CompletedRoom_13 { get; set; }

    public bool CompletedRoom_14 { get; set; }

    public bool CompletedRoom_15 { get; set; }

    public bool CompletedRoom_16 { get; set; }

    public bool CompletedRoom_21 { get; set; }

    public bool CompletedRoom_22 { get; set; }

    public GameObject CurrentRoom { get; set; }

    public Vector3 OnPlayerPos { get; set; }

    public Vector3 OffPlayerPos { get; set; }

    private GameStateMachine()
    {
        ;
    }

}
