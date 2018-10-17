using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PickupItem : MonoBehaviour
{
    Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = PickUp;
    }

    void PickUp(GameObject player) {
        Vector3 onPlayerPos = player.transform.position;
        Vector3 myPos = transform.position;
        PlayerBase playerBase = player.GetComponent<PlayerBase>();
        if (playerBase.heldItem != null) {
            GameObject heldItem = playerBase.heldItem;
            heldItem.transform.localPosition = player.transform.localPosition + new Vector3(0, 1, 1.5f);
            playerBase.heldItem = null;
            gameObject.transform.SetParent(null);
            gameObject.transform.position = Utils.NearestCubeCenter(heldItem.transform.position);
            GetComponent<Rigidbody>().isKinematic = false;
        } else if (interactable.SelectedBy() == player && (playerBase.heldItem == null || !playerBase.heldItem.Equals(gameObject))) {
            playerBase.heldItem = gameObject;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y/2, 0);
            gameObject.transform.SetParent(player.transform);
            gameObject.transform.localRotation = Quaternion.Euler(0, -45, 0);
        }
    }


}
