using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOn : MonoBehaviour {
    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -LevelController.gravity, 0);
    }
	
	void Update () {
        handleMovement();
    }

    private void handleMovement() {
        float moveHorizontal = Input.GetAxis("POnHorizontal");
        float moveVertical = Input.GetAxis("POnVertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.position = rb.position + (movement * LevelController.PlayerMovementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return) && Mathf.Abs(rb.velocity.y) < 0.05) {
            //rb.velocity = new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical);
            rb.AddForce(new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical), ForceMode.Impulse);
        }
    }
}
