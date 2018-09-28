using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void OnPlayerInteractAction();
    public static event OnPlayerInteractAction OnOnPlayerInteracted;

    public delegate void OffPlayerInteractAction();
    public static event OffPlayerInteractAction OnOffPlayerInteracted;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelController.gameGoing()) return;


        try
        {
            if (Input.GetButton("Fire1"))
            {
                OnOnPlayerInteracted();
            }
            if (Input.GetButton("Fire2"))
            {
                OnOffPlayerInteracted();
            }
        }
        catch (System.NullReferenceException)
        {
            ;
        }
    }
}
