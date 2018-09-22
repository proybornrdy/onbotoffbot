﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {
    public Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        handleMovement();
    }

    private void handleMovement() {
        float moveHorizontal = Input.GetAxis("P2Horizontal");
        float moveVertical = Input.GetAxis("P2Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement * PlayerStats.movementSpeed;
    }
}
