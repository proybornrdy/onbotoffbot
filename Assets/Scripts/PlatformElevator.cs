using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElevator : Toggleable {

	public int height = 2;
    public Transform platform;
	public bool startOn = false;
    bool on = false;
	bool raising = false;
	bool lowering = false;
	float minY;
	float maxY;
	// Use this for initialization
	void Start () {
		minY = platform.position.y;
		maxY = minY + height;
	}
	
	// Update is called once per frame
	void Update () {
		if (raising) {
			if (platform.position.y < maxY) {
				Raise();
			} else {
				raising = false;
			}
		}
		else if (lowering) {
			if (platform.position.y > minY) {
				Lower();
			} else {
				lowering = false;
			}
		}
	}
	
	void Raise() {
		 platform.position += Vector3.up * Time.deltaTime;
	}
	
	void Lower() {
		 platform.position += Vector3.down * Time.deltaTime;
	}
	
	public override void TurnOn()
    {
        if (!on)
        {
            on = true;
			raising = true;
        }
    }

    public override void TurnOff()
    {
        if (on)
        {
            on = false;
            lowering = true;
        }
    }
}
