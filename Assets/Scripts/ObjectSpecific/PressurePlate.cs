using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public Toggleable toggleable;
    int enters = 0;

    private void Toggle()
    {
        if (toggleable.IsOn()) toggleable.TurnOff();
        else toggleable.TurnOn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.HasWeight))
        {
            enters++;
            if (enters == 1) Toggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(Tag.HasWeight))
        {
            enters--;
            if (enters == 0) Toggle();
            else if (enters < 0) enters = 0;
        }
    }
}
