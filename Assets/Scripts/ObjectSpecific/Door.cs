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
        isReady = true;
        if (isOpen) TurnOn();
        else TurnOff();
	}

    public override void TurnOn()
    {
        if (!isReady) return;
        isOpen = true;
        slide.transform.localPosition = new Vector3(-0.25f, 1.5f, 0.618f);
        overLight.color = Color.blue;
        lc.DoorOpened(index);
    }

    public override void TurnOff()
    {
        if (!isReady) return;
        isOpen = false;
        slide.transform.localPosition = new Vector3(-0.25f, -0.5f, 0.618f);
        overLight.color = Color.red;
        lc.DoorClosed(index);
    }

    public override bool IsOn()
    {
        return isOpen;
    }
}
