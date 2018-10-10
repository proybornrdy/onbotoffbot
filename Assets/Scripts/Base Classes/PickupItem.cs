using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PickupItem : MonoBehaviour
{
    Interactable interactable;
    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        interactable.InteractAction = PickUp;
    }

    void PickUp(GameObject player) {
        Vector3 onPlayerPos = player.transform.position;
        Vector3 myPos = transform.position;
        PlayerBase playerBase = player.GetComponent<PlayerBase>();
        if (playerBase.heldItem != null) {
            playerBase.heldItem.gameObject.transform.position = player.transform.position + new Vector3(0, 1, 1);
            playerBase.heldItem = null;
            gameObject.transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
        } else if (interactable.SelectedBy() == player && (playerBase.heldItem == null || !playerBase.heldItem.Equals(gameObject))) {
            playerBase.heldItem = gameObject;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y/2, 0);
            gameObject.transform.SetParent(player.transform);
        }
    }


}
