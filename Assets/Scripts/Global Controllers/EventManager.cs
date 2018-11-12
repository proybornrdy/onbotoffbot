using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void InteractAction(GameObject player);
    public static event InteractAction OnInteract;
    public delegate void PickupAction(GameObject player);
    public static event PickupAction OnPickup;
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
			if (Input.GetAxis(PlayerInputTranslator.GetLeftInteract(Player.ON)) > 0.8
				|| Input.GetButton(PlayerInputTranslator.GetLeftInteract(Player.ON)))
			{
                if (Time.time - lastPressedOn > pressThreshold)
                {
                    OnInteract(LevelController.OnPlayer);
                    lastPressedOn = Time.time;
                }
            }
			if (Input.GetAxis(PlayerInputTranslator.GetRightInteract(Player.OFF)) > 0.8
				|| Input.GetButton(PlayerInputTranslator.GetRightInteract(Player.OFF)))
            {
				if (Time.time - lastPressedOff > pressThreshold)
                {
                    OnInteract(LevelController.OffPlayer);
                    lastPressedOff = Time.time;
                }
            }

            if (Input.GetButton(PlayerInputTranslator.GetReset(Player.ON)) ||
                Input.GetButton(PlayerInputTranslator.GetReset(Player.OFF)))
            {
                LevelController.ResetScene();
            }

            if (Input.GetButton(PlayerInputTranslator.GetMenu(Player.ON)) ||
                Input.GetButton(PlayerInputTranslator.GetMenu(Player.OFF)))
            {
                LevelController.GoToMenu();
            }
        }
        catch (System.NullReferenceException)
        {
            ;
        }
    }
}
