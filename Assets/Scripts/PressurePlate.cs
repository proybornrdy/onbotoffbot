using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public Toggleable toggleable;
    bool inUse = false;

    // Use this for initialization
    void Start()
    {
        ;
    }

    void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
    }

    private void Toggle()
    {
        if (toggleable.IsOn()) toggleable.TurnOff();
        else toggleable.TurnOn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inUse) return;
        inUse = true;
        Toggle();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!inUse) return;
        inUse = false;
        Toggle();
    }
}
