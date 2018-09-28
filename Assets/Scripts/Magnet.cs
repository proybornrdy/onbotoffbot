using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public float magneticRange;
    public int acceleration = 1;
    public bool startOn = false;
    public GameObject magneticObject;
    Rigidbody magneticRb;
    Rigidbody magnetRb;
    Magnetic magnetic;
    bool on = false;
    bool isColliding = false;

    // Use this for initialization
    void Start () {
        if (startOn)
        {
            TurnOn();
        }
        magnetic = magneticObject.GetComponent<Magnetic>();
        magnetRb = GetComponent<Rigidbody>();
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
        if(inPullingRange(magnetRb, magneticRb, magneticRange))
        {
            magneticRb.MovePosition(Vector3.MoveTowards(magneticRb.position, transform.position, (magnetic.speed += acceleration) * Time.deltaTime));
        }
        
    }

    bool inPullingRange(Rigidbody magnetRb, Rigidbody magneticRb, float magneticRange)
    {
        return Mathf.Abs(magnetRb.position.x - magneticRb.position.x) <= magneticRange && Mathf.Abs(magnetRb.position.z - magneticRb.position.z) <= magneticRange && Mathf.Abs(magnetRb.position.z - magneticRb.position.z) <= magneticRange;
    }
}
