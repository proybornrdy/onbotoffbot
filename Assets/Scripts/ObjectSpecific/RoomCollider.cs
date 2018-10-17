using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    int playerOnEnters = 0;
    int playerOffEnters = 0;
    LevelController lc;
    Door door;

    void Awake()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        var parent = transform.parent.gameObject;
        door = parent.GetComponentInChildren<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.PlayerOn))
            playerOnEnters++;
        else if (other.gameObject.HasTag(Tag.PlayerOff))
            playerOffEnters++;
        if (playerOnEnters > 0 && playerOffEnters > 0) lc.PlayersMovedToRoom(door.index);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(Tag.PlayerOn))
            playerOnEnters--;
        else if (other.gameObject.HasTag(Tag.PlayerOff))
            playerOffEnters--;

        if (playerOnEnters < 0) playerOnEnters = 0;
        if (playerOffEnters < 0) playerOffEnters = 0;
    }
}
