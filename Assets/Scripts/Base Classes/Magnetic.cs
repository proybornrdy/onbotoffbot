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
        }
        magneticRb = GetComponent<Rigidbody>();
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
            if(tag != "Player")
            {
                magneticRb.drag = Mathf.Infinity;
                gameObject.transform.parent = other.transform;
            }
            
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
        if (InPullingRange(magnetRb, magneticRb, magnet.maxRange))
        {
            if (tag == "Player")
            {
                float Distance = Vector3.Distance(magneticRb.transform.position, magnetRb.transform.position);
                float TDistance = Mathf.InverseLerp(magnet.maxRange, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance the object is.
                float strength = Mathf.Lerp(0f, magnet.maxStrength, TDistance); // Use that decimal to work out how much strength the magnet should apply
                Vector3 FromObjectToMagnet = (magnetRb.transform.position - magneticRb.transform.position).normalized; // Get the direction from the object to the magnet
                magneticRb.AddForce(FromObjectToMagnet * strength, ForceMode.Force); // apply force to the object
            }
            else
            {
                if(!GetIsColliding())
                {
                    magneticRb.MovePosition(Vector3.MoveTowards(magneticRb.position, magnetRb.position, (speed += acceleration) * Time.deltaTime));
                }
            }           
        }
    }

    public bool InPullingRange(Rigidbody magnetRb, Rigidbody magneticRb, float magneticRange)
    {
        return Mathf.Abs(magnetRb.position.x - magneticRb.position.x) <= 0.5 &&
            Mathf.Abs(magnetRb.position.y - magneticRb.position.y) <= magneticRange &&
            Mathf.Abs(magnetRb.position.z - magneticRb.position.z) <= magneticRange;        
    }
}
