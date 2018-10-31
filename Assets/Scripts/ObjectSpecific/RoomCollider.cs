using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    int playerOnEnters = 0;
    int playerOffEnters = 0;
    LevelController lc;
    Door door;
    TextTrigger text;
    public bool faded = false;

    void Awake()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        var parent = transform.parent.gameObject;
        door = parent.GetComponentInChildren<Door>();
        text = GetComponentInChildren<TextTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.Player))
        {
            if (other.gameObject.HasTag(Tag.PlayerOn))
                playerOnEnters++;
            else if (other.gameObject.HasTag(Tag.PlayerOff))
                playerOffEnters++;

            if (playerOnEnters > 0 && playerOffEnters > 0)
            {
				if(door) lc.PlayersMovedToRoom(door.index);
				else lc.PlayersMovedToRoom(-1);
				if (text) text.TurnOn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(Tag.Player))
        {
            if (other.gameObject.HasTag(Tag.PlayerOn))
            playerOnEnters--;
            else if (other.gameObject.HasTag(Tag.PlayerOff))
                playerOffEnters--;

            if (playerOnEnters < 0) playerOnEnters = 0;
            if (playerOffEnters < 0) playerOffEnters = 0;
            if (playerOnEnters > 0 && playerOffEnters > 0)
            {
                if (text) text.TurnOff();
            }
        }
    }
}
