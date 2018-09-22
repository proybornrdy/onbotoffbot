using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
    public Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        handleMovement();
    }

    private void handleMovement() {
        float moveHorizontal = Input.GetAxis("P1Horizontal");
        float moveVertical = Input.GetAxis("P1Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement * LevelController.PlayerMovementSpeed;
    }
}
