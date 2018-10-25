using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PickupItem : MonoBehaviour
{
    Interactable interactable;
    Transform initParent;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = PickUp;
        initParent = transform.parent;
    }

    void PickUp(GameObject player) {
        PlayerBase playerBase = player.GetComponent<PlayerBase>();

        if (playerBase.heldItem != null) {
            GameObject obj = playerBase.heldItem;
            playerBase.heldItem = null;

            obj.transform.SetParent(initParent);
            obj.transform.position = player.transform.position + new Vector3(0, 1, 1.25f);
            obj.transform.RotateAround(player.transform.position, Vector3.up, player.transform.rotation.eulerAngles.y);
            obj.transform.position = Utils.NearestCubeCenter(obj.transform.position);
            gameObject.transform.rotation = Utils.AngleSnap(gameObject.transform.rotation);
            GetComponent<Rigidbody>().isKinematic = false;
        } else if (interactable.SelectedBy() == player && (playerBase.heldItem == null || !playerBase.heldItem.Equals(gameObject))) {
            playerBase.heldItem = gameObject;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = player.transform.position + new Vector3(0, player.GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, gameObject.GetComponent<Collider>().bounds.size.y/2, 0);
            gameObject.transform.SetParent(player.transform);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


}
