using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffButton : MonoBehaviour {

	public GameObject offPlayer;
    public Toggleable[] toggleable;

    // Use this for initialization
    void Start()
    {
        ;
    }

    void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
    }


    void TurnOff()
    {
        Vector3 offPlayerPos = Utils.closesCorner(offPlayer);
		Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Utils.vectorEqual(offPlayerPos, buttonPos))
        {
            foreach (var t in toggleable)
                t.TurnOff();
        }
    }

    void OnEnable()
    {
        EventManager.OnOffPlayerInteracted += TurnOff;
    }

    void OnDisable()
    {
        EventManager.OnOffPlayerInteracted -= TurnOff;
    }
}
