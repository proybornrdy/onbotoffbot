using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Switch : MonoBehaviour {
    public Toggleable[] toggleable;
    bool on = false;
    public LineRenderer wire;
    public Light slotLight;
    public GameObject slot;
    Interactable interactable;

    // Use this for initialization
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = Toggle;
    }

    void Toggle(GameObject player)
    {
        if (on) TurnOff(player);
        else TurnOn(player);
    }


    void TurnOn(GameObject onPlayer)
    {
        if (on || !onPlayer.GetComponent<PlayerOn>()) return;
        Vector3 onPlayerPos = Utils.closesCorner(onPlayer);
        Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Utils.vectorEqual(onPlayerPos, buttonPos))
        {
            on = true;
            foreach (var t in toggleable)
                t.TurnOn();
            DynamicGI.SetEmissive(slot.GetComponent<Renderer>(), slot.GetComponent<Renderer>().material.color);
            DynamicGI.SetEmissive(wire, wire.material.color);
            slotLight.intensity = 10;
        }

    }


    void TurnOff(GameObject offPlayer)
    {
        if (!on || !offPlayer.GetComponent<PlayerOff>()) return;
        Vector3 offPlayerPos = Utils.closesCorner(offPlayer);
        Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Utils.vectorEqual(offPlayerPos, buttonPos))
        {
            on = false;
            foreach (var t in toggleable)
                t.TurnOff();
            DynamicGI.SetEmissive(slot.GetComponent<Renderer>(), Color.black);
            DynamicGI.SetEmissive(wire, Color.black);
            slotLight.intensity = 1;
        }

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
