using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable {
    public bool isOpen = false;
    public GameObject openDoor;
    public GameObject closedDoor;

	// Use this for initialization
	void Start () {
		if (isOpen)
        {
            TurnOn();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TurnOn()
    {
        isOpen = true;
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    public override void TurnOff()
    {
        isOpen = false;
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
    }

    public override bool IsOn()
    {
        return isOpen;
    }
}
