using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : Toggleable
{
    public Toggleable[] toggleables;
    public float range = 1;
    public LineRenderer laser;
    public bool startOn;
    int enters = 0;
    bool on = false;

    private void Start()
    {
        laser.transform.localScale = new Vector3(2, 2, 4 * range);
        
        if (startOn) TurnOn();
        else TurnOff();
    }

    private void ToggleControlled()
    {
        foreach (var t in toggleables) t.Toggle();
    }

    private void OnTriggerEnter(Collider other)
    {
        enters++;
        if (enters == 1) ToggleControlled();
    }

    private void OnTriggerExit(Collider other)
    {
         enters--;
        if (enters == 0) ToggleControlled();
        else if (enters < 0) enters = 0;
    }

    public override void TurnOn()
    {
        on = true;
        laser.enabled = true;
    }

    public override void TurnOff()
    {
        laser.enabled = false;
    }

    public override bool IsOn()
    {
        throw new System.NotImplementedException();
    }
}
