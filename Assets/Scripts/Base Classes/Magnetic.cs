﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour {

    public float speed;
    public float acceleration;
    public GameObject magnetObj;
    Rigidbody magneticRb;
    Rigidbody magnetRb;
    Magnet magnet;
    bool isColliding = false;

    // Use this for initialization
    void Start () {
        if(magnetObj)
        {
            magnet = magnetObj.GetComponent<Magnet>();
            magnetRb = magnetObj.GetComponent<Rigidbody>();
            magneticRb = GetComponent<Rigidbody>();
        }        
    }
	
	// Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsColliding(bool colliding)
    {
        isColliding = colliding;
    }

    public bool GetIsColliding()
    {
        return isColliding;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == magnetObj)
        {
            SetIsColliding(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == magnetObj)
        {
            if (!magnet.IsOn()) {
                SetIsColliding(false);
            }
        }
    }

    public void GetPulled()
    {
        if (InPullingRange(magnetRb, magneticRb, magnet.magneticRange))
        {
            if (tag == "Player")
            {
                Vector3 relativePos = (magnetRb.position - magneticRb.position) * 30;
                magneticRb.AddForce(relativePos, ForceMode.Acceleration);
            }
            else
            {
                magneticRb.MovePosition(Vector3.MoveTowards(magneticRb.position, magnetRb.position, (speed += acceleration) * Time.deltaTime));
            }            
        }
    }

    public bool InPullingRange(Rigidbody magnetRb, Rigidbody magneticRb, float magneticRange)
    {
        return Mathf.Abs(magnetRb.position.x - magneticRb.position.x) <= 0.1 &&
            Mathf.Abs(magnetRb.position.y - magneticRb.position.y) <= magneticRange &&
            Mathf.Abs(magnetRb.position.z - magneticRb.position.z) <= magneticRange;
    }
}