using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    public PlayerOn playerOn;
    public PlayerOff playerOff;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PickUpOnPlayer() {
        Vector3 onPlayerPos = playerOn.transform.position;
        Vector3 myPos = transform.position;
        if (Utils.InRange(onPlayerPos, myPos) && !playerOff.pickedUpItem.Equals(gameObject)) {
            playerOn.pickedUpItem = gameObject;
            Debug.Log("PLAYER ON PICKUP");
        }
    }
    void PickUpOffPlayer() {
        Vector3 offPlayerPos = playerOff.transform.position;
        Vector3 myPos = transform.position;
        if (Utils.InRange(offPlayerPos, myPos) && !playerOn.pickedUpItem.Equals(gameObject)) {
            Debug.Log("PLAYER OFF PICKUP");
            playerOff.pickedUpItem = gameObject;
        }
    }

    void OnEnable() {
        EventManager.OnOnPlayerPickedUp += PickUpOnPlayer;
        EventManager.OnOffPlayerPickedUp += PickUpOffPlayer;
    }

    void OnDisable() {
        EventManager.OnOnPlayerPickedUp -= PickUpOnPlayer;
        EventManager.OnOffPlayerPickedUp -= PickUpOffPlayer;
    }


}
