using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    public PlayerOn playerOn;
    public PlayerOff playerOff;

    void PickUpOnPlayer() {
        Vector3 onPlayerPos = playerOn.transform.position;
        Vector3 myPos = transform.position;
        if (playerOn.pickedUpItem != null) {
            playerOn.pickedUpItem.gameObject.transform.position = playerOn.transform.position + new Vector3(0, 0.5f, 1);
            playerOn.pickedUpItem = null;
            gameObject.transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
        } else if (Utils.InRange(onPlayerPos, myPos) && (playerOff.pickedUpItem == null || !playerOff.pickedUpItem.Equals(gameObject))) {
            playerOn.pickedUpItem = gameObject;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = playerOn.transform.position + new Vector3(0, playerOn.GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y/2, 0);
            gameObject.transform.SetParent(playerOn.transform);
        }
    }
    void PickUpOffPlayer() {
        Vector3 offPlayerPos = playerOff.transform.position;
        Vector3 myPos = transform.position;
        if (playerOff.pickedUpItem != null) {
            playerOff.pickedUpItem.gameObject.transform.position = playerOff.transform.position + new Vector3(0, 0.5f, 1);
            playerOff.pickedUpItem = null;
            gameObject.transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
        } else if(Utils.InRange(offPlayerPos, myPos) && (playerOn.pickedUpItem == null || !playerOn.pickedUpItem.Equals(gameObject))) {
            playerOff.pickedUpItem = gameObject;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = playerOff.transform.position + new Vector3(0, playerOff.GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y / 2, 0);
            gameObject.transform.SetParent(playerOff.transform);
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
