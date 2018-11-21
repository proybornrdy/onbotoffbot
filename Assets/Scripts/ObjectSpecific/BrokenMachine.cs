using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMachine : Toggleable {
    bool on = true;
    public ParticleSystem[] particles;
    public GameObject particleCollider;

    public override bool IsOn()
    {
        return on;
    }

    public override void TurnOff()
    {
        on = false;
        foreach (var p in particles) p.Stop();
        particleCollider.SetActive(false);
        GetComponent<AudioSource>().Stop();
    }

    public override void TurnOn()
    {
        on = true;
        foreach (var p in particles) p.Play();
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
