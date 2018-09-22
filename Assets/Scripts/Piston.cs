using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Toggleable
{

    Vector3 movement;
    Rigidbody pistonRigidbody;
    bool on;

    // Use this for initialization
    void Start () {
        pistonRigidbody = GetComponent<Rigidbody>();
        on = false;
    }

    // Update is called once per frame
    void Update () {
        
    }

    public override void TurnOn()
    {
        Extend();
    }

    public override void TurnOff()
    {
        Contract();
    }

    public void Extend()
    {
        on = true;
        movement.Set(-1f, 0f, 0f);
        pistonRigidbody.MovePosition(transform.position + movement);
    }

    public void Contract()
    {
        on = false;
        movement.Set(1f, 0f, 0f);
        pistonRigidbody.MovePosition(transform.position + movement);
    }
}
