using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void InteractAction(GameObject player);
    public static event InteractAction OnInteract;
    float lastPressedOn = 0;
    float lastPressedOff = 0;
    public float pressThreshold = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelController.gameGoing()) return;
        
        try
        {
            if (Input.GetAxis("OnInteract") > 0.8 || Input.GetButton("OnInteract"))
            {
                if (Time.time - lastPressedOn > pressThreshold)
                {
                    OnInteract(LevelController.OnPlayer);
                    lastPressedOn = Time.time;
                }
            }
            if (Input.GetAxis("OffInteract") > 0.8 || Input.GetButton("OffInteract"))
            {
                if (Time.time - lastPressedOff > pressThreshold)
                {
                    OnInteract(LevelController.OffPlayer);
                    lastPressedOff = Time.time;
                }
            }
        }
        catch (System.NullReferenceException)
        {
            ;
        }
    }
}
