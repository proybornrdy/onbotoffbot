using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    internal Rigidbody rb;

    public GameObject pickedUpItem;
    internal string horizontalAxis;
    internal string verticalAxis;
    internal string jump;
    internal string interact;

    void Start()
    {
        Physics.gravity = new Vector3(0, -LevelController.gravity, 0);
    }

    void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
        handleMovement();
    }

    private void handleMovement()
    {
        //float moveHorizontal = Input.GetAxis(horizontalAxis);
        //float moveVertical = Input.GetAxis(verticalAxis);

        Vector2 moveVec = movementRotationFix(Input.GetAxis(verticalAxis), -1 * Input.GetAxis(horizontalAxis));

        if (moveVec != Vector2.zero)
        {
            float moveHorizontal = moveVec.y;
            float moveVertical = moveVec.x;

            transform.rotation = Quaternion.LookRotation(new Vector3(-moveHorizontal, 0, moveVertical), Vector3.up);

            // Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            // rb.position = rb.position + (movement * LevelController.PlayerMovementSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveVertical * LevelController.moveSpeed, relativeTo: Space.World);
            transform.Translate(Vector3.left * moveHorizontal * LevelController.moveSpeed, relativeTo: Space.World);

            if (Input.GetButton(jump) && Mathf.Abs(rb.velocity.y) < 0.05)
            {
                rb.velocity = new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical);
                // rb.AddForce(new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical), ForceMode.Impulse);
            }
        }
    }

    private Vector2 movementRotationFix(float h, float v)
    {
        Vector2 vec = new Vector2(h, v);
        return Quaternion.Euler(0, 0, -45) * vec;
    }
}