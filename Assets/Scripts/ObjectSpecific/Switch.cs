using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Switch : MonoBehaviour {
    public Toggleable[] toggleable;
    public bool on = false;
    public LineRenderer wire;
    public Light slotLight;
    public GameObject slot;
    Interactable interactable;

    // Use this for initialization
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = Toggle;
        if (on) TurnOn();
        else TurnOff();
    }

    void Toggle(GameObject player)
    {
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
        DynamicGI.SetEmissive(slot.GetComponent<Renderer>(), slot.GetComponent<Renderer>().material.color);
        if (wire) DynamicGI.SetEmissive(wire, wire.material.color);
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
        DynamicGI.SetEmissive(slot.GetComponent<Renderer>(), Color.black);
        if (wire) DynamicGI.SetEmissive(wire, Color.black);
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
