using UnityEngine;

public class OptoinsSceenController : MonoBehaviour
{
	public GameObject Options;
	public GameObject Credits;
	public GameObject LeaderBoard;
	public GameObject PlayerSelect;

	// Use this for initialization
	void Start()
	{
		StartPlayerSelect();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void StartCredits()
	{
		Options.SetActive(false);
		Credits.SetActive(true);
		(Credits.GetComponent<CreditRunner>()).Restart_Scroll();
	}

	void StopCredits()
	{
		Options.SetActive(true);
		Credits.SetActive(false);
	}

	void StartLeaderBoard()
	{
		Options.SetActive(false);
		LeaderBoard.SetActive(true);
		(LeaderBoard.GetComponent<LeaderBoard>()).updateLeaderBoard();
	}

	void StopLeaderBoard()
	{
		Options.SetActive(true);
		LeaderBoard.SetActive(false);
	}

	void StartPlayerSelect()
	{
		Options.SetActive(false);
		PlayerSelect.SetActive(true);
		PlayerSelect.GetComponent<PlayerSelect>().SetUp();
	}
}
