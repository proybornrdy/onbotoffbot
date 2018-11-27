using UnityEngine;

public class Door : Toggleable {
    public bool isOpen = false;
    public GameObject slide;
    public Light overLight;
    public int index = -1;
    LevelController lc;
    bool isReady = false;
    bool muteSoundOnInit = true;
    Color color;

    // Use this for initialization
    void Start ()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        isReady = true;
        if (isOpen) Open();
        else Close();
	}

    public override void TurnOn()
    {
        if (!isReady) return;
        isOpen = true;
        Open();
        lc.DoorOpened(index);
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("DoorOn");
        else muteSoundOnInit = false;
    }

    public override void TurnOff()
    {
        if (!isReady) return;
        isOpen = false;
        Close();
        lc.DoorClosed(index);
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("DoorOff");
        else muteSoundOnInit = false;
    }

    void Open()
    {
        slide.transform.localPosition = new Vector3(-0.25f, 1.5f, 0.618f);
        overLight.color = Color.blue;
    }

    void Close()
    {
        slide.transform.localPosition = new Vector3(-0.25f, -0.5f, 0.618f);
        overLight.color = Color.red;
    }

    public override bool IsOn()
    {
        return isOpen;
    }
}
