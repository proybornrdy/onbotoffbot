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
		if (!LevelController.gameGoing())
		{
			return;
		}
		Vector3 offPlayerPos = Utils.closesCorner(offPlayer);
		Vector3 buttonPos = Utils.closesCorner(this.gameObject);
		if (Input.GetAxis("Button Off") > .5 && Utils.vectorEqual(offPlayerPos, buttonPos))
        {
            toggleable.TurnOff();
        }
    }
}
