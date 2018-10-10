using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void InteractAction(GameObject player);
    public static event InteractAction OnInteract;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelController.gameGoing()) return;
        
        try
        {
            if (Input.GetButton("OnInteract"))
            {
                OnInteract(LevelController.OnPlayer);
            }
            if (Input.GetButton("OffInteract"))
            {
                OnInteract(LevelController.OffPlayer);
            }
        }
        catch (System.NullReferenceException)
        {
            ;
        }
    }
}
