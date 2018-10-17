using UnityEngine;

public class OptoinsSceenController : MonoBehaviour
{
	public GameObject Options;
	public GameObject Credits;
	public GameObject LeaderBoard;
	public GameObject PlayerSelect;
	public GameObject LevelSelect;

	public GameObject OnBot;
	public GameObject OffBot;

	// Use this for initialization
	void Start()
	{
		StartMain();
		// StartPlayerSelect();
	}

	public void StartMain()
	{
		Options.SetActive(true);
		Options.GetComponent<OptionsScreen>().SetUp();
	}

	public void StopMain()
	{
		Options.SetActive(false);
	}

	public void StartCredits()
	{
		Credits.SetActive(true);
		(Credits.GetComponent<CreditRunner>()).Restart_Scroll();
	}

	public void StopCredits()
	{
		Credits.SetActive(false);
	}

	public void StartLeaderBoard()
	{
		LeaderBoard.SetActive(true);
		(LeaderBoard.GetComponent<LeaderBoard>()).updateLeaderBoard();
	}

	public void StopLeaderBoard()
	{
		LeaderBoard.SetActive(false);
	}

	public void StartPlayerSelect()
	{
		PlayerSelect.SetActive(true);
		PlayerSelect.GetComponent<PlayerSelect>().SetUp();
	}

	public void StopPlayerSelect()
	{
		PlayerSelect.SetActive(false);
	}

	public void StartLevelSelect()
	{
		LevelSelect.SetActive(true);
		LevelSelect.GetComponent<LevelSelect>().StartUp();
	}

	public void StopLevelSelect()
	{
		LevelSelect.SetActive(false);
	}
}
