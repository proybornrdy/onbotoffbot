using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPushableBlock : MonoBehaviour {

    Rigidbody rigidBody;
    bool onFloor = true;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFloor)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }
}
