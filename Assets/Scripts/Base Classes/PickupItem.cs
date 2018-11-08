using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PickupItem : MonoBehaviour
{
    Interactable interactable;
    public Transform initParent;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = OnInteract;
        initParent = transform.parent;
    }

    void OnInteract(GameObject player)
    {
        ;
    }
}
