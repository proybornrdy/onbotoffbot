using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GridSnap : MonoBehaviour {

    //distance from center of square required to snap
    public float distanceThreshold = 0.1f;
    //maximum speed at which snapping can happen
    public float speedThreshold = 1e7f;
    //maximum falling speed at which snapping is forced
    public float fallVelocityThreshold = 0.1f;
    public float yVelocity;

    RigidbodyConstraints initialConstraints;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        initialConstraints = rb.constraints;
	}
	
	// Update is called once per frame
	void Update () {
        //only check velocity in x/z, still want snapping when object is falling
        yVelocity = rb.velocity.y;
        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 nearestCube = Utils.NearestCubeCenter(transform.position);
        if ((rb.velocity.y <= fallVelocityThreshold) || velocity.magnitude <= speedThreshold && Vector3.Distance(transform.position, nearestCube) <= distanceThreshold)
        {
            if (rb.velocity.y <= fallVelocityThreshold)
            {
                rb.constraints = initialConstraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                rb.constraints = initialConstraints;
            }
            transform.position = new Vector3(nearestCube.x, transform.position.y, nearestCube.z);
        }
    }
}
