using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffButton : MonoBehaviour {

    public Toggleable toggleable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2"))
        {
            toggleable.TurnOff();
        }
    }
}
