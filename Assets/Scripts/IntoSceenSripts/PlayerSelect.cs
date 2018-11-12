using UnityEngine;
using System;

public class PlayerSelect : MonoBehaviour
{
	public GameObject P1Select;
	public GameObject P1SelectLeft;
	public GameObject P1SelectRight;
	private bool resetP1 = true;

	public GameObject P2Select;
	public GameObject P2SelectLeft;
	public GameObject P2SelectRight;
	private bool resetP2 = true;

	public float CenterPostion;
	public float OnPosition;
	public float OffPosition;

	public GameObject SceneController;

	public void SetUp()
	{
		P1Select.transform.position = new Vector3(CenterPostion, P1Select.transform.position.y, P1Select.transform.position.z);
		P2Select.transform.position = new Vector3(CenterPostion, P2Select.transform.position.y, P2Select.transform.position.z);

		P1SelectLeft.SetActive(true);
		P1SelectRight.SetActive(true);
		P2SelectLeft.SetActive(true);
		P2SelectRight.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("P1Horizontal") < -0.5f && resetP1 && CanMoveLeft(P1Select))
		{
			resetP1 = false;
			if (P1Select.transform.position.x > CenterPostion)
			{
				P1Select.transform.position = new Vector3(CenterPostion, P1Select.transform.position.y, P1Select.transform.position.z);
				P1SelectLeft.SetActive(true);
				P1SelectRight.SetActive(true);
			}
			else
			{
				P1Select.transform.position = new Vector3(OnPosition, P1Select.transform.position.y, P1Select.transform.position.z);
				P1SelectLeft.SetActive(false);
				P1SelectRight.SetActive(true);
			}
		}
		else if (Input.GetAxis("P1Horizontal") > 0.5f && resetP1 && CanMoveRight(P1Select))
		{
			resetP1 = false;
			if (P1Select.transform.position.x < CenterPostion)
			{
				P1Select.transform.position = new Vector3(CenterPostion, P1Select.transform.position.y, P1Select.transform.position.z);
				P1SelectLeft.SetActive(true);
				P1SelectRight.SetActive(true);
			}
			else
			{
				P1Select.transform.position = new Vector3(OffPosition, P1Select.transform.position.y, P1Select.transform.position.z);
				P1SelectLeft.SetActive(true);
				P1SelectRight.SetActive(false);
			}
		}

		if (Input.GetAxis("P2Horizontal") < -0.5f && resetP2 && CanMoveLeft(P2Select))
		{
			resetP2 = false;
			if (P2Select.transform.position.x > CenterPostion)
			{
				P2Select.transform.position = new Vector3(CenterPostion, P2Select.transform.position.y, P2Select.transform.position.z);
				P2SelectLeft.SetActive(true);
				P2SelectRight.SetActive(true);
			}
			else
			{
				P2Select.transform.position = new Vector3(OnPosition, P2Select.transform.position.y, P2Select.transform.position.z);
				P2SelectLeft.SetActive(false);
				P2SelectRight.SetActive(true);
			}
		}
		else if (Input.GetAxis("P2Horizontal") > 0.5f && resetP2 && CanMoveRight(P2Select))
		{
			resetP2 = false;
			if (P2Select.transform.position.x < CenterPostion)
			{
				P2Select.transform.position = new Vector3(CenterPostion, P2Select.transform.position.y, P2Select.transform.position.z);
				P2SelectLeft.SetActive(true);
				P2SelectRight.SetActive(true);
			}
			else
			{
				P2Select.transform.position = new Vector3(OffPosition, P2Select.transform.position.y, P2Select.transform.position.z);
				P2SelectLeft.SetActive(true);
				P2SelectRight.SetActive(false);
			}
		}

		if (Input.GetAxis("P1Horizontal") < .5 && Input.GetAxis("P1Horizontal") > -.5)
		{
			resetP1 = true;
		}
		if (Input.GetAxis("P2Horizontal") < .5 && Input.GetAxis("P2Horizontal") > -.5)
		{
			resetP2 = true;
		}

		if (Input.GetAxis("P1Jump") > .5 || Input.GetButton("P1Jump") || Input.GetAxis("P2Jump") > .5 || Input.GetButton("P2Jump"))
		{
			confirm();
		}

		if (Input.GetButton("P1Back") || Input.GetButton("P2Back") || Input.GetButton("Back"))
		{
			SceneController.GetComponent<OptoinsSceenController>().StopPlayerSelect();
			SceneController.GetComponent<OptoinsSceenController>().StartMain();
		}
	}

	private bool CanMoveLeft(GameObject selectionObject)
	{
		return selectionObject.transform.position.x > OnPosition;
	}

	private bool CanMoveRight(GameObject selectionObject)
	{
		return selectionObject.transform.position.x < OffPosition;
	}

	private void confirm()
	{
		if ((Math.Abs(P1Select.transform.position.x - OnPosition) < .1 && Math.Abs(P2Select.transform.position.x - OffPosition) < .1) ||
			(Math.Abs(P1Select.transform.position.x - OffPosition) < .1 && Math.Abs(P2Select.transform.position.x - OnPosition) < .1))
		{
			if (P1Select.transform.position.x == OnPosition)
			{
				// P1 can move left, so they must be off bot
				PlayerInputTranslator.SetP1P2(Player.ON, Player.OFF);
			}
			else
			{
				PlayerInputTranslator.SetP1P2(Player.OFF, Player.ON);
			}

			SceneController.GetComponent<OptoinsSceenController>().StopPlayerSelect();
			SceneController.GetComponent<OptoinsSceenController>().StartLevelSelect();
		}
	}
}
