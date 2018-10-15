using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    internal Rigidbody rb;
    public float selectionThreshold = 120;

    public GameObject heldItem;
    public GameObject selectedItem;
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
        Vector2 moveVec = movementRotationFix(Input.GetAxis(verticalAxis), -1 * Input.GetAxis(horizontalAxis));

        float moveHorizontal = moveVec.y;
        float moveVertical = moveVec.x;

        Vector3 moveDirection = new Vector3(-moveHorizontal, 0, moveVertical);

        if (moveVec != Vector2.zero)
        {
			transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }

		float dampening_factor = 1;
		if (Mathf.Abs(rb.velocity.y) >= 0.05) // in the air
		{
			dampening_factor = LevelController.flightDampener;
		}
		transform.Translate(Vector3.forward * moveVertical * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);
        transform.Translate(Vector3.left * moveHorizontal * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);

        if (Input.GetButton(jump) && Mathf.Abs(rb.velocity.y) < 0.05)
        {
            rb.velocity = new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical);
        }

        if (heldItem != null)
        {
            selectedItem = heldItem;
        }
        else
        {
            if (selectedItem != null)
            {
                selectedItem.GetComponent<Interactable>().Deselect();
                selectedItem = null;
            }

            var interactables = TagCatalogue.FindAllWithTag(Tag.Interactable)
                .Where(obj => Utils.InRange(transform.position, obj.transform.position))
                .OrderBy(obj => Vector3.Angle(moveDirection, obj.transform.position - transform.position));
            if (interactables.Count() != 0)
            {
                GameObject closest = interactables.ElementAt(0);
                if (Vector3.Angle(moveDirection, closest.transform.position - transform.position)
                    <= selectionThreshold)
                {
                    closest.GetComponent<Interactable>().Select(gameObject);
                    selectedItem = closest;
                }
            }
        }
        
    }

    private Vector2 movementRotationFix(float h, float v)
    {
        Vector2 vec = new Vector2(h, v);
        return Quaternion.Euler(0, 0, -45) * vec;
    }

    private void Select()
    {

    }
}