using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour {

	public GameObject onPlayer;
    public Toggleable toggleable;

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
        Vector3 onPlayerPos = onPlayer.transform.position;
        Vector3 buttonPos = transform.position;
        if (Utils.InRange(onPlayerPos, buttonPos))
        {
            toggleable.TurnOn();
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
