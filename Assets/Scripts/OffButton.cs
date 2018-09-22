using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffButton : MonoBehaviour {

	public GameObject offPlayer;
    public Toggleable toggleable;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		Vector3 offPlayerPos = offPlayer.transform.position;
		Vector3 buttonPos = transform.position;
        if (Input.GetButton("Fire2")&&
			System.Math.Pow(offPlayerPos.x - buttonPos.x, 2) <= 1 &&
			System.Math.Pow(offPlayerPos.y - buttonPos.y, 2) <= 1 &&
			System.Math.Pow(offPlayerPos.z - buttonPos.z, 2) <= 1)
        {
			print(offPlayerPos);
			print(buttonPos);
            toggleable.TurnOff();
        }
    }
}
