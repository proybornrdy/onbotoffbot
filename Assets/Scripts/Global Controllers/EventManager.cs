using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void OnPlayerInteractAction();
    public static event OnPlayerInteractAction OnOnPlayerInteracted;

    public delegate void OffPlayerInteractAction();
    public static event OffPlayerInteractAction OnOffPlayerInteracted;

    public delegate void OnPlayerPickupAction();
    public static event OnPlayerPickupAction OnOnPlayerPickedUp;

    public delegate void OffPlayerPickupAction();
    public static event OffPlayerPickupAction OnOffPlayerPickedUp;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelController.gameGoing()) return;


        try
        {
            if (Input.GetButton("Button On"))
            {
                OnOnPlayerInteracted();
            }
            if (Input.GetButton("Button Off"))
            {
                OnOffPlayerInteracted();
            }
        }
        catch (System.NullReferenceException)
        {
            ;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            OnOnPlayerPickedUp();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnOffPlayerPickedUp();
        }
    }
}
