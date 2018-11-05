using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMachine : Toggleable {
    bool on = true;
    public GameObject[] particles;
    public GameObject particleCollider;

    public override bool IsOn()
    {
        return on;
    }

    public override void TurnOff()
    {
        on = false;
        foreach (var p in particles) p.SetActive(false);
        particleCollider.SetActive(false);
        GetComponent<AudioSource>().Stop();
    }

    public override void TurnOn()
    {
        on = true;
        foreach (var p in particles) p.SetActive(true);
        particleCollider.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    // Use this for initialization
    void Start () {
        if (on) TurnOn();
        if (!on) TurnOff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
