using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    GameObject onPlayer;
    GameObject offPlayer;
    public Toggleable[] toggleable;
    bool on = false;
    public LineRenderer wire;
    public Light slotLight;
    public GameObject slot;

    // Use this for initialization
    void Start()
    {
        onPlayer = LevelController.OnPlayer;
        offPlayer = LevelController.OffPlayer;
        TurnOff();
    }


    void TurnOn()
    {
        if (on) return;
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


    void TurnOff()
    {
        if (!on) return;
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
        EventManager.OnOnPlayerInteracted += TurnOn;
        EventManager.OnOffPlayerInteracted += TurnOff;
    }

    void OnDisable()
    {
        EventManager.OnOnPlayerInteracted -= TurnOn;
        EventManager.OnOffPlayerInteracted -= TurnOff;
    }
}
