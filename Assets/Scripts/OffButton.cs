using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffButton : MonoBehaviour {

	public GameObject offPlayer;
    public Toggleable toggleable;

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
        Vector3 offPlayerPos = offPlayer.transform.position;
        Vector3 buttonPos = transform.position;
        if (Utils.InRange(offPlayerPos, buttonPos))
        {
            toggleable.TurnOff();
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
