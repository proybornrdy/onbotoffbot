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
    Color color;
    bool isReady;

    // Use this for initialization
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = Toggle;
        slotMat = slot.GetComponent<Renderer>().material;
        lightMat = wire.material;
        color = slotMat.color;
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
            t.TurnOn();

        Material slotMat = slot.GetComponent<Renderer>().material;
        slotMat.SetColor("_EmissionColor", color);
        if (wire) lightMat.SetColor("_EmissionColor", color);
        slotLight.intensity = 10;
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
