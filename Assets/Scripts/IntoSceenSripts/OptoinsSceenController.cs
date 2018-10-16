using UnityEngine;
using UnityEngine.SceneManagement;

public class OptoinsSceenController : MonoBehaviour
{
	public GameObject Options;
	public GameObject Credits;
	public GameObject LeaderBoard;

	public string next_sceen_name;

	// Use this for initialization
	void Start()
	{
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

	void SelectStart()
	{
		SceneManager.LoadSceneAsync(next_sceen_name);
	}

	void StartleaderBoard()
	{
		Options.SetActive(false);
		LeaderBoard.SetActive(true);
		(LeaderBoard.GetComponent<LeaderBoard>()).updateLeaderBoard();
	}

	void StopleaderBoard()
	{
		Options.SetActive(true);
		LeaderBoard.SetActive(false);
	}
}
