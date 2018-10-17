using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OffButton : MonoBehaviour {
    public Toggleable[] toggleable;
    Interactable interactable;

    // Use this for initialization
    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        interactable.InteractAction = TurnOff;
    }

    void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
    }


    void TurnOff(GameObject player)
    {
        if (!player.HasTag(Tag.PlayerOff)) return;
        Vector3 offPlayerPos = player.transform.position;
		Vector3 buttonPos = gameObject.transform.position;
        if (Utils.InRange(offPlayerPos, buttonPos))
        {
            foreach (var t in toggleable)
                t.TurnOff();
        }
    }
}
