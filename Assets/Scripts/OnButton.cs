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
		if (!LevelController.gameGoing())
		{
			return;
		}
		Vector3 onPlayerPos = Utils.closesCorner(onPlayer);
		Vector3 buttonPos = Utils.closesCorner(this.gameObject);
        if (Input.GetAxis("Button On") > .5 && Utils.vectorEqual(onPlayerPos, buttonPos))
        {
            toggleable.TurnOn();
        }
    }
}
