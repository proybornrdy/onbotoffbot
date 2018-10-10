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
        Vector3 onPlayerPos = Utils.closesCorner(player);
		Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Utils.vectorEqual(onPlayerPos, buttonPos))
        {
            foreach (var t in toggleable)
                t.TurnOn();
        }
    }
}
