using UnityEngine;
using UnityEngine.SceneManagement;

public class OptoinsSceenController : MonoBehaviour
{
	public GameObject Options;
	public GameObject Credits;

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

	void ClickStart()
	{
		SceneManager.LoadSceneAsync(next_sceen_name);
	}
}
