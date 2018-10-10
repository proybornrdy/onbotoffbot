using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RailPushed : MonoBehaviour {
    Rigidbody rb;
    RigidbodyConstraints initialConstraints;
    int plateCollisions = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        initialConstraints = rb.constraints;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Tags tags = collision.gameObject.GetComponent<Tags>();
        if (!tags) return;
        if (tags.HasTag(Tag.PressurePlate))
            plateCollisions += 1;
        if (plateCollisions == 1)
        {
            Vector3 pushDirection = collision.collider.transform.position - transform.position;
            Vector3 cardinal = Utils.NearestCardinal(pushDirection);
            //Get object onto pressure plate
            transform.position += (cardinal + Vector3.up) * 0.2f;
        }
        //Don't move if collider isn't allowed to push
        else if (!tags.HasTag(Tag.CanPush))
        {
            rb.constraints = initialConstraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            Vector3 pushDirection;
            if (tags.HasTag(Tag.ConveyerBelt))
            {
                pushDirection = collision.gameObject.transform.right;
            }
            else
                pushDirection = transform.position - collision.collider.transform.position;
            Utils.Coordinate largestComponent = Utils.LargestComponent(pushDirection);
            if (largestComponent == Utils.Coordinate.x)
            {
                rb.constraints = initialConstraints | RigidbodyConstraints.FreezePositionZ;
            }
            else if (largestComponent == Utils.Coordinate.y)
            {
                rb.constraints = initialConstraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                rb.constraints = initialConstraints | RigidbodyConstraints.FreezePositionX;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = initialConstraints;
        plateCollisions--;
        if (plateCollisions < 0) plateCollisions = 0;
    }
}
