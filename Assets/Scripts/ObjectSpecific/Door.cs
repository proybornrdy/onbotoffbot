using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable {
    public bool isOpen = false;
    public GameObject slide;
    public Light overLight;
    public int index = -1;
    LevelController lc;
    bool isReady = false;

    // Use this for initialization
    void Start ()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        if (isOpen) TurnOn();
        else TurnOff();
        isReady = true;
	}

    public override void TurnOn()
    {
        if (!isReady) return;
        isOpen = true;
        slide.transform.localPosition = new Vector3(-0.25f, 1.5f, 0.618f);
        overLight.color = new Color(0, 1, 0);
        lc.DoorOpened(index);
    }

    public override void TurnOff()
    {
        if (!isReady) return;
        isOpen = false;
        slide.transform.localPosition = new Vector3(-0.25f, -0.5f, 0.618f);
        overLight.color = new Color(1, 0, 0);
        lc.DoorClosed(index);
    }

    public override bool IsOn()
    {
        return isOpen;
    }
}
