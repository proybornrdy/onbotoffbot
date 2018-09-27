using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public int magneticRange = 4;
    public int acceleration = 1;
    public bool startOn = false;
    public GameObject magneticObject;
    Rigidbody magneticRb;
    Magnetic magnetic;
    bool on = false;
    bool isColliding = false;

    // Use this for initialization
    void Start () {
		magnetic = magneticObject.GetComponent<Magnetic>();
        magneticRb = magneticObject.GetComponent<Rigidbody>();
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
            magneticRb.drag = Mathf.Infinity;
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            magneticRb.drag = 0;
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
        //magneticObject.transform.position = Vector3.MoveTowards(magneticObject.transform.position, transform.position, (magnetic.speed += acceleration) * Time.deltaTime);
        magneticRb.MovePosition(Vector3.MoveTowards(magneticRb.position, transform.position, (magnetic.speed += acceleration) * Time.deltaTime));

    }
}
