using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public Toggleable[] toggleables;
    Transform prevParent;
    int enters = 0;

    private void ToggleControlled()
    {
        foreach (var t in toggleables) t.Toggle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(Tag.HasWeight))
        {
            enters++;
            if (enters == 1)
            {
                ToggleControlled();
                prevParent = other.transform.parent;
                other.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(Tag.HasWeight))
        {
            enters--;
            if (enters == 0) {
                ToggleControlled();
                other.transform.parent = prevParent;
            }
            else if (enters < 0) enters = 0;
        }
    }
}
