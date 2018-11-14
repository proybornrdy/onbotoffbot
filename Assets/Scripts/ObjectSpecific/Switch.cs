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
    //Color color;
    Material onMat;
    Material offMat;
    bool isReady;
    bool muteSoundOnInit = true;

    // Use this for initialization
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = Toggle;
        //slotMat = slot.GetComponent<Renderer>().material;
        //lightMat = wire.material;
        //color = slotMat.color;
        onMat = (Material)Resources.Load("Wire Materials/Blue", typeof(Material));
        offMat = (Material)Resources.Load("Wire Materials/Red", typeof(Material));
        print(onMat);
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

        //slotMat.SetColor("_EmissionColor", color);
        //if (wire) lightMat.SetColor("_EmissionColor", color);
        //slotLight.intensity = 10;
        slot.GetComponent<Renderer>().material = onMat;
        if (wire) wire.material = onMat;
        slotLight.color = onMat.color;
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
        //slotMat.SetColor("_EmissionColor", Color.black);
        //if (wire) lightMat.SetColor("_EmissionColor", Color.black);
        //slotLight.intensity = 1;
        slot.GetComponent<Renderer>().material = offMat;
        if (wire) wire.material = offMat;
        slotLight.color = offMat.color;
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
