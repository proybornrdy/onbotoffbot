using UnityEngine;
using System.Collections;

public class Door : Toggleable {
    public bool isOpen = false;
    public GameObject slide;
    public Light overLight;
    public int index = -1;
    LevelController lc;
    bool isReady = false;
    bool muteSoundOnInit = true;
    Color color;
    public float openSpeed;
    bool closing = false;
    bool opening = false;

    // Use this for initialization
    void Start ()
    {
        lc = GameObject.Find("LevelController")?.GetComponent<LevelController>();
        isReady = true;
        if (isOpen) Open();
        else Close();
	}

    public override void TurnOn()
    {
        if (!isReady) return;
        isOpen = true;
        Open();
        lc?.DoorOpened(index);
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("DoorOn");
        else muteSoundOnInit = false;
    }

    public override void TurnOff()
    {
        if (!isReady) return;
        isOpen = false;
        Close();
        lc?.DoorClosed(index);
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("DoorOff");
        else muteSoundOnInit = false;
    }

    void Open()
    {
        StopCoroutine("CloseAnimation");
        StartCoroutine("OpenAnimation");
        if (overLight)
            overLight.color = Color.blue;
    }

    void Close()
    {
        StopCoroutine("OpenAnimation");
        StartCoroutine("CloseAnimation");
        if (overLight)
            overLight.color = Color.red;
    }

    public override bool IsOn()
    {
        return isOpen;
    }

    IEnumerator OpenAnimation()
    {
        while (slide.transform.localPosition.y < 1.5f)
        {
            slide.transform.localPosition += Vector3.up * Time.deltaTime * openSpeed;
            yield return null;
        }
    }

    IEnumerator CloseAnimation()
    {
        while (slide.transform.localPosition.y > -0.5f)
        {
            slide.transform.localPosition += Vector3.down * Time.deltaTime * openSpeed;
            yield return null;
        }
    }
}
