using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable {
    public bool isOpen = false;
    public GameObject openDoor;
    public GameObject closedDoor;
    public Light overLight;

    // Use this for initialization
    void Start () {
        if (isOpen) TurnOn();
        else TurnOff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TurnOn()
    {
        isOpen = true;
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        overLight.color = new Color(0, 1, 0);
    }

    public override void TurnOff()
    {
        isOpen = false;
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        overLight.color = new Color(1, 0, 0);
    }

    public override bool IsOn()
    {
        return isOpen;
    }
}
