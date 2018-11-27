using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magnet : Toggleable {
    
    public float maxRange = 5f; // maximum distance the magnet will begin pulling an object from
    public float maxStrength = 100f; // Maximum strength the magnet will pull something right next to it. Goes down as the object gets further away

    public bool startOn = false;
    bool on = false;
    public Light spotLight;
    public GameObject pullPoint;
    List<GameObject> children;
    List<GameObject> prevParents;

    // Use this for initialization
    void Start () {
        children = new List<GameObject>();
        prevParents = new List<GameObject>();
        spotLight.range = maxRange;
        if (startOn)
        {
            TurnOn();            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() {
    }

    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
            var objs = TagCatalogue.FindAllWithTag(Tag.Magnetic).Where(o => InMaxRange(o));
            foreach (var o in objs)
            {
                StartCoroutine("Pull", o);
            }
            print("Magnet is ON");
        }
    }

    IEnumerator Pull(GameObject obj)
    {
        Vector3 init = obj.transform.position;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Rigidbody>().useGravity = false;
        for (int i = 0; i < 10; i++)
        {
            obj.transform.position = Vector3.Lerp(init, pullPoint.transform.position, i / 10f);
            yield return null;
        }
        children.Add(obj);
        prevParents.Add(obj.transform.parent == null ? null : obj.transform.parent.gameObject);
        obj.transform.SetParent(transform);
        yield return null;
    }

    //void Pull()
    //{
    //    var objs = TagCatalogue.FindAllWithTag(Tag.Magnetic).Where(o => InMaxRange(o));
    //    foreach (var o in objs)
    //    {
    //        //o.transform.position = Vector3.MoveTowards(o.transform.position, pullPoint.transform.position, maxStrength * Time.deltaTime);
    //        var rb = o.GetComponent<Rigidbody>();
    //        rb.velocity = pullPoint.transform.position - o.transform.position;
    //    }
        
    //}

    void Stop()
    {
        //var objs = TagCatalogue.FindAllWithTag(Tag.Magnetic).Where(o => InMaxRange(o));
        //foreach (var o in objs)
        //{
        //    print(o);
        //    var rb = o.GetComponent<Rigidbody>();
        //    rb.isKinematic = false;
        //}
        for (int i = 0; i < children.Count; i++)
        {
            children[i].transform.SetParent(prevParents[i] ==  null ? null : prevParents[i].transform);
            children[i].GetComponent<Rigidbody>().isKinematic = false;
            children[i].GetComponent<Rigidbody>().useGravity = true;
        }
        children.Clear();
        prevParents.Clear();
    }

    bool InMaxRange(GameObject obj)
    {
        //use a raycast so that range will only have to reach the collider, not the object's origin.
        //will only travel as far as the magnet's range
        print(obj.transform.position);
        print(pullPoint.transform.position);
        if (obj.transform.position == pullPoint.transform.position) return true;
        var hits = Physics.RaycastAll(pullPoint.transform.position, obj.transform.position - pullPoint.transform.position, maxRange).OrderBy(h => h.distance);
        if (hits.Count() == 0) return false;//|| hits.ElementAt(0).transform != obj.transform) return false; // there is something in front of it
        return true;
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            print("Magnet is OFF");
            Stop();
        }
    }

    public override bool IsOn()
    {
        return on;
    }

    //void OnTriggerEnter(Collider other) {
    //    Rail yRail = transform.parent.GetComponent<Rail>();
    //    if (yRail) {
    //        Rail zRail = yRail.transform.parent.GetComponent<Rail>();
    //        if (zRail) {Rail xRail = zRail.transform.parent.GetComponent<Rail>();
    //            if (xRail) {
    //                //Al of this runs only if the magnet is attached to a 3-axis rail system.
    //                GameObject railSystem = GameObject.Find("RailSystem");
    //                if ((!other.transform.IsChildOf(railSystem.transform) || other.transform.IsChildOf(transform)) && other.name != "RoomCollider") {
    //                    if (CollidedOnY(other)) {
    //                        yRail.SetDirection(-yRail.GetDirection());
    //                    } else if (CollidedOnZ(other)) {
    //                        zRail.SetDirection(-zRail.GetDirection());
    //                    } else if (CollidedOnX(other)) {
    //                        xRail.SetDirection(-xRail.GetDirection());
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other) {
    //    Rail yRail = transform.parent.GetComponent<Rail>();
    //    if (yRail && other.transform.IsChildOf(transform) && transform.position.y - transform.GetComponent<BoxCollider>().bounds.size.y / 2 + 0.05 < other.transform.position.y + other.transform.GetComponent<BoxCollider>().bounds.size.y / 2) {
    //        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
    //        other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 0.05f, other.transform.position.z);
    //        yRail.SetDirection(-yRail.GetDirection());
    //    }
    //}

    //bool CollidedOnY(Collider other) { return (CollisionDirection(other.gameObject) == "y"); }

    //bool CollidedOnZ(Collider other) { return (CollisionDirection(other.gameObject) == "z"); }

    //bool CollidedOnX(Collider other) { return (CollisionDirection(other.gameObject) == "x"); }

    //private string CollisionDirection(GameObject other) {
    //    RaycastHit RayHit;
    //    Vector3 direction = (gameObject.transform.position - other.transform.position).normalized;
    //    Ray MyRay = new Ray(other.transform.position, direction);
    //    if (Physics.Raycast(MyRay, out RayHit)) {
    //        Vector3 HitNormal = RayHit.normal;
    //        HitNormal = RayHit.transform.TransformDirection(HitNormal);
    //        if (HitNormal == RayHit.transform.up || HitNormal == -RayHit.transform.up) { return "y"; }
    //        else if (HitNormal == RayHit.transform.forward || HitNormal == -RayHit.transform.forward) { return "z"; }
    //        else if (HitNormal == RayHit.transform.right || HitNormal == -RayHit.transform.right) { return "x"; }
    //    }
    //    Debug.LogError("shouldn't be getting here");
    //    return "";
    //}
}
