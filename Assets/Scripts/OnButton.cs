using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour {

	public GameObject onPlayer;
    public Toggleable toggleable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 onPlayerPos = onPlayer.transform.position;
		Vector3 buttonPos = transform.position;
        if (Input.GetButton("Fire1") &&
			System.Math.Pow(onPlayerPos.x - buttonPos.x, 2) <= 1 &&
			System.Math.Pow(onPlayerPos.y - buttonPos.y, 2) <= 1 &&
			System.Math.Pow(onPlayerPos.z - buttonPos.z, 2) <= 1)
        {
            toggleable.TurnOn();
        }
    }
}
