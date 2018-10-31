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
    Animator a;
    public JumpPoint jumpPoint;
    internal string horizontalAxis;
    internal string verticalAxis;
    internal string jump;
    internal string interact;
    internal string reset;
    bool isGrounded;

    int jumpFrames = 24;
    Vector3? jumpFrom = null;
    Vector3? jumpTo = null;
    bool jumping = false;

    public void Start()
    {
        a = GetComponent<Animator>();
        print(a);
        Physics.gravity = new Vector3(0, -LevelController.gravity, 0);
    }

    public void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
        handleMovement();
    }

    void OnCollisionStay()
    {
        if (!isGrounded && Mathf.Abs(rb.velocity.y) < 0.005) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void handleMovement()
    {
        Vector2 moveVec = movementRotationFix(Input.GetAxis(verticalAxis), -1 * Input.GetAxis(horizontalAxis));

        float moveHorizontal = moveVec.y;
        float moveVertical = moveVec.x;

        Vector3 moveDirection = new Vector3(-moveHorizontal, 0, moveVertical);

        if (moveVec != Vector2.zero)
        {
            var target = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);

            a.Play("Walking");
        }

		if (!LevelController.snapJumpingStatic)
        {
            float dampening_factor = 1;
            if (Mathf.Abs(rb.velocity.y) >= 0.05) // in the air
            {
                dampening_factor = LevelController.flightDampener;
            }
            transform.Translate(Vector3.forward * moveVertical * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);
            transform.Translate(Vector3.left * moveHorizontal * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);

            if (Input.GetButton(jump) && isGrounded) //)
            {
                rb.velocity = new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical);
            }
        }
        //else if (isGrounded)
        else if (!jumping)
        {
            if (Input.GetButton(jump))// && moveDirection != Vector3.zero)
            {
                var jps = TagCatalogue.FindAllWithTag(Tag.JumpPoint)
                    .Where(obj => obj.transform.parent != this.transform && Utils.InJumpRange(transform.position, obj.transform.position) &&
                                    Vector3.Angle(transform.rotation * Vector3.forward, obj.transform.position - transform.position) <= selectionThreshold)
                    .OrderBy(obj => -Utils.NearestCubeCenter(obj.transform.position).y)
                    .ThenBy(obj => Vector3.Angle(transform.rotation * Vector3.forward, obj.transform.position - transform.position));
                if (jps.Count() > 0)
                {
                    jumping = true;
                    jumpFrom = transform.position;
                    jumpTo = jps.ElementAt(0).transform.position;
                    StartCoroutine("Jump");
                }
            }
            float dampening_factor = 1;
            if (Mathf.Abs(rb.velocity.y) >= 0.05) // in the air
            {
                dampening_factor = LevelController.flightDampener;
            }
            transform.Translate(Vector3.forward * moveVertical * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);
            transform.Translate(Vector3.left * moveHorizontal * LevelController.moveSpeed * dampening_factor, relativeTo: Space.World);
        }

        Select(moveDirection);
        
    }

    IEnumerator Jump()
    {
        Vector3 midPoint = new Vector3(
            (jumpFrom.Value.x + jumpTo.Value.x) / 2,
            ((jumpFrom.Value.y + jumpTo.Value.y) / 2) + 1,
            (jumpFrom.Value.z + jumpTo.Value.z) / 2);

        for (int i = 0; i < jumpFrames; i++)
        {
            float t = ((float)i) / jumpFrames;
            transform.position = ((1 - t) * (1 - t) * jumpFrom.Value) + (2 * (1 - t) * t * midPoint) + (t * t * jumpTo.Value);
            yield return null;
        }
        jumping = false;
        jumpFrom = null;
        jumpTo = null;
        yield return null;
    }

    private Vector2 movementRotationFix(float h, float v)
    {
        Vector2 vec = new Vector2(h, v);
        return Quaternion.Euler(0, 0, -45) * vec;
    }

    private void Select(Vector3 moveDirection)
    {
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

    private void JumpCoroutine()
    {

    }
}