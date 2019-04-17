using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElevator : Toggleable {

	public int height = 2;
    public Transform platform;
	public bool startOn = false;
    public bool on = false;
	bool raising = false;
	bool lowering = false;
	float minY;
	float maxY;
	// Use this for initialization
	void Start () {
		minY = platform.position.y;
		maxY = minY + height;
        if (startOn)
        {
            on = true;
            platform.position += Vector3.up * height;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (raising) {
            Debug.Log("RAISEEEE  " + platform.position.y + "   " + maxY);
			if (platform.position.y < maxY) {
				Raise();
			} else {
				raising = false;
			}
		}
		if (lowering) {
			if (platform.position.y > minY) {
                //if(!PlayerUnderneath())
                //{
                    Lower();
                //}				
			} else {
				lowering = false;
			}
		}
	}
	
	void Raise() {
        bool inWay = false;
        var hits = Physics.RaycastAll(platform.position + (Vector3.back * 0.5f), platform.up, 0.1f);
        foreach (var h in hits)
        {
            if (h.transform != platform.transform) inWay = true;
        }
        //if (!inWay) platform.position += Vector3.up * Time.deltaTime * 2;
        platform.position += Vector3.up * Time.deltaTime * 2;
    }
	
	void Lower() {
        bool inWay = false;
        var hits = Physics.RaycastAll(platform.position + (Vector3.back * 0.5f), platform.up * -1, 0.1f);
        foreach (var h in hits)
        {
            if (h.transform != platform.transform) inWay = true;
        }
		if (!inWay) platform.position += Vector3.down * Time.deltaTime * 2;
	}

    /*bool PlayerUnderneath()
    {
        Vector3 origin = platform.transform.position + new Vector3(0, 0, 0.6f);
        Vector3 direction = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;
        if (Physics.SphereCast(origin, 0.8f, direction, out hit, 3F))
        {
            if (hit.collider.gameObject.HasTag(Tag.Player))
            {
                return true;
            }
        }
        return false;
    }*/


    public override void TurnOn()
    {
        if (!on)
        {
            on = true;
			raising = true;
            lowering = false;
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            lowering = true;
            raising = false;
        }
    }

    public override bool IsOn()
    {
        return on;
    }
}
