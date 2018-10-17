using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OnButton : MonoBehaviour 
{
    public Toggleable[] toggleable;
    Interactable interactable;

    // Use this for initialization
    void Awake() {
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        interactable.InteractAction = TurnOn;
    }
    // Update is called once per frame
    void Update () {
		if (!LevelController.gameGoing())
		{
			return;
		}
    }
    

    void TurnOn(GameObject player)
    {
        if (!player.HasTag(Tag.PlayerOn)) return;
        Vector3 offPlayerPos = player.transform.position;
        Vector3 buttonPos = gameObject.transform.position;
        if (Utils.InRange(offPlayerPos, buttonPos))
        {
            foreach (var t in toggleable)
                t.TurnOn();
        }
    }
}
