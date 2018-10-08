using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour {

	public GameObject OnPlayer;
    public Toggleable[] toggleable;

	// Use this for initialization
	void Start () {
        ;
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
        Vector3 onPlayerPos = OnPlayer.transform.position;
        Vector3 buttonPos = transform.position;
        if (Utils.InRange(onPlayerPos, buttonPos))
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
