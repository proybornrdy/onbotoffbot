using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceScript : MonoBehaviour {

    Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

	
	// Update is called once per frame
	void Update () {
		rb.AddForce(10, 10, 10);
    }
}
