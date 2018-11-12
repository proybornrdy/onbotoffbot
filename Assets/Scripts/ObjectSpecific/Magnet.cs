using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Toggleable {

    public float maxRange = 5f; // maximum distance the magnet will begin pulling an object from
    public float maxStrength = 100f; // Maximum strength the magnet will pull something right next to it. Goes down as the object gets further away

    public bool startOn = false;
    public GameObject[] magneticObjects;
    bool on = false;

    // Use this for initialization
    void Start () {
        if (startOn)
        {
            TurnOn();            
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        if (on)
        {
            foreach (GameObject magneticObject in magneticObjects)
            {
                Magnetic magnetic = magneticObject.GetComponent<Magnetic>();
                magnetic.GetPulled();
            }
        }
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
            print("Magnet is ON");
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            print("Magnet is OFF");
            foreach (GameObject magneticObject in magneticObjects)
            {
                Rigidbody magneticRb = magneticObject.GetComponent<Rigidbody>();
                if (magneticObject.tag != "Player" && magneticRb.drag == Mathf.Infinity)
                {
                    magneticRb.drag = 0;
                    magneticObject.transform.parent = null;
                }
                magneticObject.GetComponent<Magnetic>().SetIsColliding(false);
            }
        }
    }

    public override bool IsOn()
    {
        return on;
    }

    void OnTriggerEnter(Collider other) {
        Rail yRail = transform.parent.GetComponent<Rail>();
        if (yRail) {
            Rail zRail = yRail.transform.parent.GetComponent<Rail>();
            if (zRail) {Rail xRail = zRail.transform.parent.GetComponent<Rail>();
                if (xRail) {
                    //Al of this runs only if the magnet is attached to a 3-axis rail system.
                    GameObject railSystem = GameObject.Find("RailSystem");
                    if ((!other.transform.IsChildOf(railSystem.transform) || other.transform.IsChildOf(transform)) && other.name != "RoomCollider") {
                        if (CollidedOnY(other)) {
                            yRail.SetDirection(-yRail.GetDirection());
                        } else if (CollidedOnZ(other)) {
                            zRail.SetDirection(-zRail.GetDirection());
                        } else if (CollidedOnX(other)) {
                            xRail.SetDirection(-xRail.GetDirection());
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        Rail yRail = transform.parent.GetComponent<Rail>();
        if (yRail && other.transform.IsChildOf(transform) && transform.position.y - transform.GetComponent<BoxCollider>().bounds.size.y / 2 + 0.05 < other.transform.position.y + other.transform.GetComponent<BoxCollider>().bounds.size.y / 2) {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 0.05f, other.transform.position.z);
            yRail.SetDirection(-yRail.GetDirection());
        }
    }

    bool CollidedOnY(Collider other) { return (CollisionDirection(other.gameObject) == "y"); }

    bool CollidedOnZ(Collider other) { return (CollisionDirection(other.gameObject) == "z"); }

    bool CollidedOnX(Collider other) { return (CollisionDirection(other.gameObject) == "x"); }

    private string CollisionDirection(GameObject other) {
        RaycastHit RayHit;
        Vector3 direction = (gameObject.transform.position - other.transform.position).normalized;
        Ray MyRay = new Ray(other.transform.position, direction);
        if (Physics.Raycast(MyRay, out RayHit)) {
            Vector3 HitNormal = RayHit.normal;
            HitNormal = RayHit.transform.TransformDirection(HitNormal);
            if (HitNormal == RayHit.transform.up || HitNormal == -RayHit.transform.up) { return "y"; }
            else if (HitNormal == RayHit.transform.forward || HitNormal == -RayHit.transform.forward) { return "z"; }
            else if (HitNormal == RayHit.transform.right || HitNormal == -RayHit.transform.right) { return "x"; }
        }
        Debug.LogError("shouldn't be getting here");
        return "";
    }
}
