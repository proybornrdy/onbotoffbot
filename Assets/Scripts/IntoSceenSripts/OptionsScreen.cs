using UnityEngine;

public class OptionsScreen : MonoBehaviour
{
	public GameObject SceneController;
	public GameObject selector;

	public Vector2 StartPostion;
	public Vector2 LeaderBoardPosition;
	public Vector2 CreditsPosition;

	private bool resetP1 = true;
	private bool resetP2 = true;

	public void SetUp()
	{
		selector.transform.localPosition = StartPostion;
	}

	// Update is called once per frame
	void Update () {
		Vector2 selecterPosition = selector.transform.localPosition;
		if (Input.GetAxis("P1Vertical") > .5 && resetP1 || Input.GetAxis("P2Vertical") > .5 && resetP2)
		{
			resetP1 = !(Input.GetAxis("P1Vertical") > .5);
			resetP2 = !(Input.GetAxis("P2Vertical") > .5);
			if (selecterPosition.x == StartPostion.x && selecterPosition.y == StartPostion.y)
			{
				selector.transform.localPosition = LeaderBoardPosition;
			}
			else if (selecterPosition.x == LeaderBoardPosition.x && selecterPosition.y == LeaderBoardPosition.y)
			{
				selector.transform.localPosition = CreditsPosition;
			}
		}

		if (Input.GetAxis("P1Vertical") < -.5 && resetP1 || Input.GetAxis("P2Vertical") < -.5 && resetP2)
		{
			resetP1 = !(Input.GetAxis("P1Vertical") < -.5);
			resetP2 = !(Input.GetAxis("P2Vertical") < -.5);
			if (selecterPosition.x == CreditsPosition.x && selecterPosition.y == CreditsPosition.y)
			{
				selector.transform.localPosition = LeaderBoardPosition;
			}
			else if (selecterPosition.x == LeaderBoardPosition.x && selecterPosition.y == LeaderBoardPosition.y)
			{
				selector.transform.localPosition = StartPostion;
			}
		}

		if (Input.GetAxis("P1Vertical") > -.5 && Input.GetAxis("P1Vertical") < .5)
		{
			resetP1 = true;
		}
		if (Input.GetAxis("P2Vertical") > -.5 && Input.GetAxis("P2Vertical") < .5)
		{
			resetP2 = true;
		}

		if (Input.GetAxis("P1Jump") > .5 || Input.GetButton("P1Jump") || Input.GetAxis("P1Jump") > .5 || Input.GetButton("P2Jump"))
		{
			Select();
		}
	}

	void Select()
	{
		Vector2 selecterPosition = selector.transform.localPosition;
		if (selecterPosition.x == StartPostion.x && selecterPosition.y == StartPostion.y)
		{
			SceneController.GetComponent<OptoinsSceenController>().StopMain();
			SceneController.GetComponent<OptoinsSceenController>().StartPlayerSelect();
		}
		else if (selecterPosition.x == LeaderBoardPosition.x && selecterPosition.y == LeaderBoardPosition.y)
		{
			SceneController.GetComponent<OptoinsSceenController>().StopMain();
			SceneController.GetComponent<OptoinsSceenController>().StartLeaderBoard();
		}
		else
		{
			SceneController.GetComponent<OptoinsSceenController>().StopMain();
			SceneController.GetComponent<OptoinsSceenController>().StartCredits();
		}
	}
}
