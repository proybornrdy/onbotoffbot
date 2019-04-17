using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour {

    public abstract void TurnOn();

    public abstract void TurnOff();

    public abstract bool IsOn();

    public void Toggle()
    {
        if (IsOn())
        {
            Debug.Log("turned off");
            TurnOff();
        }
        else
        {
            Debug.Log("turned on");
            TurnOn();
        }

    }
}
