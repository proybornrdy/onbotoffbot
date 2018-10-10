using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour
{

    GameObject onPlayer;
    public Toggleable[] toggleable;

    // Use this for initialization
    void Start()
    {
        onPlayer = LevelController.OnPlayer;
    }

    // Update is called once per frame
    void Update () {
		if (!LevelController.gameGoing())
		{
			return;
		}
    }
    

    void TurnOn()
    {
        Vector3 onPlayerPos = Utils.closesCorner(onPlayer);
		Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Utils.vectorEqual(onPlayerPos, buttonPos))
        {
            foreach (var t in toggleable)
                t.TurnOn();
        }
    }

    void OnEnable()
    {
        EventManager.OnOnPlayerInteracted += TurnOn;
    }

    void OnDisable()
    {
        EventManager.OnOnPlayerInteracted -= TurnOn;
    }
}
