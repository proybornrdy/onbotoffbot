using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Switch : MonoBehaviour {
    public Toggleable[] toggleable;
    public bool on = false;
    public LineRenderer wire = null ;
    public Light slotLight;
    public GameObject slot;
    Material slotMat;
    Material lightMat;
    Interactable interactable;
    public Color color;
    bool isReady;
    bool muteSoundOnInit = true;

    // Use this for initialization
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = Toggle;
        slotMat = slot.GetComponent<Renderer>().material;
        slotMat.color = color;
        if (wire)
        {
            lightMat = wire.material;
            lightMat.color = color;
        }
        slotLight.color = slotMat.color;
        if (on) TurnOn();
        else TurnOff();
        isReady = true;
    }

    void Toggle(GameObject player)
    {
        if (!isReady) return;
        if (on) TurnOff(player);
        else TurnOn(player);
    }


    void TurnOn(GameObject onPlayer)
    {
        if (on || !onPlayer.HasTag(Tag.PlayerOn)) return;
        Vector3 onPlayerPos = onPlayer.transform.position;
        Vector3 buttonPos = transform.position;
        if (Utils.InRange(onPlayerPos, buttonPos))
        {
            TurnOn();
        }
    }

    void TurnOn()
    {
        on = true;
        foreach (var t in toggleable)
        {
            t.TurnOn();
        }

        slotMat.SetColor("_EmissionColor", color);
        if (wire) lightMat.SetColor("_EmissionColor", color);
        slotLight.intensity = 20;
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("SwitchOn");
        else muteSoundOnInit = false;
    }


    void TurnOff(GameObject offPlayer)
    {
        if (!on || !offPlayer.HasTag(Tag.PlayerOff)) return;
        Vector3 offPlayerPos = offPlayer.transform.position;
        Vector3 buttonPos = transform.position;
        if (Utils.InRange(offPlayerPos, buttonPos))
        {
            TurnOff();
        }
    }

    void TurnOff()
    {
        on = false;
        foreach (var t in toggleable)
            t.TurnOff();
        slotMat.SetColor("_EmissionColor", Color.black);
        if (wire) lightMat.SetColor("_EmissionColor", Color.black);
        slotLight.intensity = 1;
        if (!muteSoundOnInit) SoundController.instance.playSoundEffect("SwitchOff");
        else muteSoundOnInit = false;
    }

    void OnEnable()
    {
        EventManager.OnInteract += TurnOn;
        EventManager.OnInteract += TurnOff;
    }

    void OnDisable()
    {
        EventManager.OnInteract -= TurnOn;
        EventManager.OnInteract -= TurnOff;
    }
}
