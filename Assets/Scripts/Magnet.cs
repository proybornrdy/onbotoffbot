using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public int magneticRange = 4;
    public int acceleration = 1;
    public bool startOn = false;
    public GameObject magneticObject;
    Magnetic magnetic;
    bool on = false;
    bool isColliding = false;

    // Use this for initialization
    void Start () {
		magnetic = magneticObject.GetComponent<Magnetic>();
    }
	
	// Update is called once per frame
	void Update () {
        if (on && !isColliding)
        {
            Pull();
        }

    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == magneticObject) {
            isColliding = true;
        }
    }

    bool getState()
    {
        return on;
    }

    void Pull()
    {
        magneticObject.transform.position = Vector3.MoveTowards(magneticObject.transform.position, transform.position, (magnetic.speed += acceleration) * Time.deltaTime);
    }
}
