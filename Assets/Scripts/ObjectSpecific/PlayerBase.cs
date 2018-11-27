using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    internal Rigidbody rb;
    public float selectionThreshold = 120;

    public bool animations;
    public PickupItem heldItem;
    public GameObject selectedItem;
    internal Animator animator;
    public JumpPoint jumpPoint;
    internal string horizontalAxis;
    internal string verticalAxis;
    internal string jump;
    internal string interact;
    internal string reset;
    internal string pickUp;
    public GameObject jumpArrow;
    GameObject jumpArrowInstance;
    public Quaternion lookRotation;
    public GameObject dropIndicator;
    GameObject dropIndicatorInstance;
    bool isGrounded;

    int jumpFrames = 24;
    Vector3? jumpFrom = null;
    Vector3? jumpTo = null;
    bool jumping = false;
    public float pickupCooldown = 1f;
    float lastPickup = 0f;
    public float movementSpeed;

    public void Start()
    {
        jumpArrowInstance = Instantiate(jumpArrow);
        jumpArrowInstance.GetComponent<Renderer>().enabled = false;
        animator = transform.GetComponent<Animator>();
        dropIndicatorInstance = Instantiate(dropIndicator);
        dropIndicatorInstance.GetComponent<Renderer>().enabled = false;


        if (animations) animator.SetBool("Walking", false);
    }

    public void Update()
    {
        if (!LevelController.gameGoing())
        {
            return;
        }
        handleMovement();
        if (Input.GetButton(pickUp))
        {
            if (Time.time - lastPickup >= pickupCooldown)
            {
                lastPickup = Time.time;
                PickUp();
            }
        }
        if (heldItem != null)
        {
            PlaceDropIndicator();
        }

        if (Input.GetButtonDown(interact)) 
        {
            if (animations) 
            {
                animator.SetTrigger("Press Button");
            }
        }
    }

    void OnCollisionStay()
    {
        if (!isGrounded && Mathf.Approximately(rb.velocity.y, 0)) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void handleMovement()
    {
		if (Time.deltaTime == 0)
		{
			return;
		}
		Vector2 moveVec = movementRotationFix(Input.GetAxis(verticalAxis), -1 * Input.GetAxis(horizontalAxis));

        float moveHorizontal = moveVec.y;
        float moveVertical = moveVec.x;

        Vector3 moveDirection = new Vector3(-moveHorizontal, 0, moveVertical);

        if (moveVec != Vector2.zero)
        {
            lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

            if (animations) animator.SetBool("Walking", true);
            Select(moveDirection);
        }
        else
        {
            if (animations) animator.SetBool("Walking", false);
        }

		if (!LevelController.snapJumpingStatic)
        {
            float dampening_factor = 1;
            if (Mathf.Abs(rb.velocity.y) >= 0.05) // in the air
            {
                dampening_factor = LevelController.flightDampener;
            }
            transform.Translate(Vector3.forward * moveVertical * movementSpeed * dampening_factor * Time.deltaTime, relativeTo: Space.World);
            transform.Translate(Vector3.left * moveHorizontal * movementSpeed * dampening_factor * Time.deltaTime, relativeTo: Space.World);

            if (Input.GetButton(jump) && isGrounded) //)
            {
                rb.velocity = new Vector3(moveHorizontal, LevelController.PlayerJumpHeight, moveVertical);
            }
        }
        //else if (isGrounded)
        else if (!jumping)
        {
            JumpSelect(moveDirection);
            if (Input.GetButton(jump))// && moveDirection != Vector3.zero)
            {
                if (jumpTo.HasValue)
                {
                    jumping = true;
                    StartCoroutine("Jump");
                }
            }
            float dampening_factor = 1;
            if (Mathf.Abs(rb.velocity.y) >= 0.05) // in the air
            {
                dampening_factor = LevelController.flightDampener;
            }
			Vector3 translation = (moveDirection * movementSpeed * dampening_factor);
            var hits = Physics.BoxCastAll(transform.position + (Vector3.up * 0.65f), new Vector3(0.2f, 0.5f, 0.2f), translation, Quaternion.Euler(transform.forward), 0.25f);
            bool inWay = false;
            foreach (var h in hits)
            {
                if (h.transform != this.transform)
                {
                    inWay = true;
                    break;
                }
            }
            if (!inWay) {
                transform.Translate(translation, relativeTo: Space.World);
            }
        }
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
        if (selectedItem != null)
        {
            selectedItem.GetComponent<Interactable>().Deselect();
            selectedItem = null;
        }

        var interactables = TagCatalogue.FindAllWithTag(Tag.Interactable)
            .Where(obj => Utils.InRange(transform.position, obj.transform.position) && (heldItem == null || !obj.Equals(heldItem.gameObject)))
            .OrderBy(obj => Mathf.Abs(Vector3.Angle(transform.forward, obj.transform.position - transform.position)));

        foreach (var i in TagCatalogue.FindAllWithTag(Tag.Interactable)
            .Where(obj => Utils.InRange(transform.position, obj.transform.position) && (heldItem == null || !obj.Equals(heldItem.gameObject))))
            Debug.DrawRay(transform.position, i.transform.position - transform.position);
        if (interactables.Count() != 0)
        {
            GameObject closest = interactables.ElementAt(0);
            Vector3 v = closest.transform.position - transform.position;
            v.y = 0;
            //print(v);
            if (Vector3.Angle(transform.forward, v)
                <= selectionThreshold)
            {
                closest.GetComponent<Interactable>().Select(gameObject);
                selectedItem = closest;
            }
        }
    }

    void PickUp() {

        //drop held item
        if (heldItem != null) {
            Vector3 newPos = Utils.NearestCubeQuarterCenter(transform.position + transform.forward + transform.up);
            var hits = Physics.RaycastAll(transform.position + transform.up, newPos, 1f);
            bool inWay = false;
            foreach( var h in hits)
            {
                if (!h.transform.gameObject.Equals(heldItem.gameObject)) inWay = true;
            }
            if (!inWay)
            {
                PickupItem item = heldItem;
                heldItem = null;

                item.transform.SetParent(item.initParent);
                item.transform.position = newPos;
                item.transform.rotation = Utils.AngleSnap(item.transform.rotation);
                item.GetComponent<Rigidbody>().isKinematic = false;
                dropIndicatorInstance.GetComponent<Renderer>().enabled = false;
            }

        } else {
            if (selectedItem && selectedItem.IsPickup())
            {
                var item = selectedItem.GetComponent<PickupItem>();
                heldItem = item;
                heldItem.GetComponent<Rigidbody>().isKinematic = true;
                heldItem.transform.position = transform.position + new Vector3(0, GetComponent<Collider>().bounds.size.y, 0) + new Vector3(0, item.GetComponent<Collider>().bounds.size.y / 2, 0);
                heldItem.transform.SetParent(transform);
                heldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                selectedItem = null;
                heldItem.GetComponent<Interactable>().Deselect();
                dropIndicatorInstance.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void JumpSelect(Vector3 moveDirection)
    {
        var jps = TagCatalogue.FindAllWithTag(Tag.JumpPoint)
                    .Where(obj => obj.transform.parent != this.transform && Utils.InJumpRange(transform.position, obj.transform.position) &&
                                    Vector3.Angle(transform.rotation * Vector3.forward, obj.transform.position - transform.position) <= selectionThreshold &&
                                    Utils.NearestCubeCenter(obj.transform.position).y > Utils.NearestCubeCenter(transform.position).y)
                    .OrderBy(obj => Vector3.Angle(transform.rotation * Vector3.forward, obj.transform.position - transform.position));
        if (jps.Count() > 0)
        {
            jumpFrom = transform.position;
            if (!isHoldingObj(jps.ElementAt(0))) {
                jumpTo = jps.ElementAt(0).transform.position;
                jumpArrowInstance.GetComponent<Renderer>().enabled = true;
                Vector3 direction = transform.position - jumpTo.Value;
                direction.y = 0;
                direction = Utils.NearestCardinal(direction) * 0.5f;
                if (direction.z == 0) jumpArrowInstance.transform.rotation = Quaternion.Euler(0, 90, 0);
                else jumpArrowInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
                Vector3 jumpArrowPos = jumpTo.Value + (Vector3.down * 0.5f) + direction;
                jumpArrowInstance.transform.position = jumpArrowPos;
            }
        }
        else
        {
            jumpTo = null;
            jumpFrom = null;
            jumpArrowInstance.GetComponent<Renderer>().enabled = false;
        }
    }

    private bool isHoldingObj(GameObject gameObject)
    {
        GameObject gameObjectParent = gameObject.transform.parent.gameObject;
        if (gameObjectParent.HasTag(Tag.Player))
        {
            foreach(Transform child in gameObjectParent.transform)
            {
                if(child.gameObject.HasTag(Tag.Pickupable))
                {
                    return true;
                }
            }            
        }
        return false;
    }

    private void PlaceDropIndicator()
    {
        Vector3 newPos = Utils.NearestCubeQuarterCenter(transform.position + transform.forward + transform.up);
        var hits = Physics.RaycastAll(transform.position + transform.up, newPos, 1f);
        bool inWay = false;
        foreach (var h in hits)
        {
            if (!h.transform.gameObject.Equals(heldItem.gameObject)) inWay = true;
        }
        if (!inWay)
        {
            RaycastHit hit;
            Physics.Raycast(newPos, Vector3.down, out hit);
            dropIndicatorInstance.transform.position = hit.point + (Vector3.up * 0.2f);
        }
    }
}