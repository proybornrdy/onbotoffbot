using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
	public GameObject P1Select;
	public GameObject P2Select;
	public float CenterPostion;
	public float OnPosition;
	public float OffPosition;
	public GameObject SceenController;

	public void SetUp()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("P1Horizontal") < -0.5f && CanMoveLeft(P1Select))
		{
			if (P1Select.transform.position.x > CenterPostion)
			{
				P1Select.transform.position = new Vector3(CenterPostion, P1Select.transform.position.y, P1Select.transform.position.z);
			}
			else
			{
				P1Select.transform.position = new Vector3(OnPosition, P1Select.transform.position.y, P1Select.transform.position.z);
			}
		}
		else if (Input.GetAxis("P1Horizontal") > 0.5f && CanMoveRight(P1Select))
		{
			if (P1Select.transform.position.x < CenterPostion)
			{
				P1Select.transform.position = new Vector3(CenterPostion, P1Select.transform.position.y, P1Select.transform.position.z);
			}
			else
			{
				P1Select.transform.position = new Vector3(OffPosition, P1Select.transform.position.y, P1Select.transform.position.z);
			}
		}

		if (Input.GetAxis("P2Horizontal") < -0.5f && CanMoveLeft(P2Select))
		{
			if (P2Select.transform.position.x > CenterPostion)
			{
				P2Select.transform.position = new Vector3(CenterPostion, P2Select.transform.position.y, P2Select.transform.position.z);
			}
			else
			{
				P2Select.transform.position = new Vector3(OnPosition, P2Select.transform.position.y, P2Select.transform.position.z);
			}
		}
		else if (Input.GetAxis("P2Horizontal") > 0.5f && CanMoveRight(P2Select))
		{
			if (P2Select.transform.position.x < CenterPostion)
			{
				P2Select.transform.position = new Vector3(CenterPostion, P2Select.transform.position.y, P2Select.transform.position.z);
			}
			else
			{
				P2Select.transform.position = new Vector3(OffPosition, P2Select.transform.position.y, P2Select.transform.position.z);
			}
		}

		if (Input.GetAxis("P1Jump") > .5 || Input.GetButton("P1Jump") || Input.GetAxis("P2Jump") > .5 || Input.GetButton("P2Jump"))
		{
			confirm();
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
		if ((CanMoveLeft(P1Select) ^ CanMoveRight(P1Select)) && (CanMoveLeft(P2Select) ^ CanMoveRight(P2Select)))
		{
			if (CanMoveLeft(P1Select))
			{
				// P1 can move left, so they must be off bot
				PlayerInputTranslator.SetP1P2(PlayerInputTranslator.Player.OFF, PlayerInputTranslator.Player.ON);
			}
			else
			{
				PlayerInputTranslator.SetP1P2(PlayerInputTranslator.Player.ON, PlayerInputTranslator.Player.OFF);
			}

			SceenController.GetComponent<OptoinsSceenController>().StopPlayerSelect();
			SceenController.GetComponent<OptoinsSceenController>().StartLevelSelect();
		}
	}
}
