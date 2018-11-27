using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : Toggleable {
    public GameObject child;
    public float speed = 2f;
    public float maxOffset = 0;
    public float snappingSpeed = 0.015f;
    float offset = 0;
    int direction = 1;
    bool on = false;
    public Axis axis = Axis.x;
    bool snapped = true;
    Vector3 moveFrom;
    Vector3 moveTo;
    Vector3 initalPos;
    Vector3 maxPos;
    float t = 0;
    int numIncrements;
    int i;
    bool paused = false;
    bool reverse = false;


    // Use this for initialization
    void Start ()
    {
        moveFrom = child.transform.position;
        moveTo = child.transform.position;
        i = 0;
        numIncrements = (int)(Mathf.Abs(maxOffset) * 2);
        if (maxOffset < 0) reverse = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (on && !paused)
        {
            if (t >= 1)
            {
                paused = true;
                StartCoroutine("Pause");
            }
            else
            {
                MoveToNewPosition();
            }
        }
        else if (t < 1 && !paused) //move to next position, then stop
        {
            MoveToNewPosition();
        }
    }

    void MoveToNewPosition()
    {
        Vector3 pos = Vector3.Lerp(moveFrom, moveTo, t);
        child.transform.position = pos;
        t += speed * Time.deltaTime;
    }

    public override bool IsOn()
    {
        return on;
    }

    public override void TurnOff()
    {
        on = false;
    }

    public override void TurnOn()
    {
        on = true;
        snapped = false;
    }

    public void SetDirection(int dir) {
        direction = dir;
    }

    public int GetDirection() {
        return direction;
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        paused = false;
        SetNextPosition();
    }

    void SetNextPosition()
    {
        float increment = direction * 0.5f;
        if (i == numIncrements) direction = -1;
        else if (i < 0) direction = 1;
        moveFrom = moveTo;
        moveTo = (moveFrom + ((axis == Axis.x? Vector3.right : axis == Axis.z? Vector3.forward : Vector3.up) * (reverse ? -1 : 1) * increment));
        print("MoveFrom: " + moveFrom + ", MoveTo: " + moveTo);
        i += direction;
        t = 0;
    }
}
