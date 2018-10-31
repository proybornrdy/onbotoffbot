using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	public GameObject LevelsContainer;

	public float StartHeight;
	public float BufferedHeight;

	private List<string[]> Levels;
	private int Selected = 0;
	private bool resetP1 = true;
	private bool resetP2 = true;
	private float startTime;
	private float myBufferedHeight;

	public Font textFont;

	public GameObject SceneController;

	public void Start()
	{
		Levels = new List<string[]>();

		//{"Level Name", "Path To Level"}
		/*
		Levels.Add(new string[]{"Level 1", "Scenes/Level 1"});

		Levels.Add(new string[] { "Alex", "Scenes/Test/Alex" });
		Levels.Add(new string[] { "Julia", "Scenes/Test/Julia" });
		Levels.Add(new string[] { "Max", "Scenes/Test/Max" });
		Levels.Add(new string[] { "Monica", "Scenes/Test/Monica" });
		Levels.Add(new string[] { "Roy", "Scenes/Test/Roy" });

		Levels.Add(new string[] { "2-7", "Scenes/Progression chunks/2-7" });
		Levels.Add(new string[] { "Section 1", "Scenes/Progression chunks/Section 1" });

		Levels.Add(new string[] { "2-6", "Scenes/Level Ideas/2-6" });
		Levels.Add(new string[] { "BasicPistonPuzzle", "Scenes/Level Ideas/BasicPistonPuzzle" });
		Levels.Add(new string[] { "Elevator", "Scenes/Level Ideas/Elevator" });
		Levels.Add(new string[] { "Magnet", "Scenes/Level Ideas/Magnet" });
		Levels.Add(new string[] { "Piston2", "Scenes/Level Ideas/Piston2/" });
		Levels.Add(new string[] { "PressurePlateLevel", "Scenes/Level Ideas/PressurePlateLevel" });*/

		Levels.Add(new string[] { "Section 1", "Scenes/Progression chunks/Section 1" });
		Levels.Add(new string[] { "PressurePlateLevel", "Scenes/Level Ideas/PressurePlateLevel" });
		Levels.Add(new string[] { "2-6", "Scenes/Level Ideas/2-6" });
		Levels.Add(new string[] { "BasicPistonPuzzle", "Scenes/Level Ideas/BasicPistonPuzzle" });
	}

	public void StartUp()
	{
		startTime = Time.time;
		if (Levels == null)
		{
			Start();
		}

		// remove all old entries
		foreach (Transform child in LevelsContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < Levels.Count; i++)
		{
			GameObject UItextGO = new GameObject("Text2");
			UItextGO.transform.SetParent(LevelsContainer.transform);

			RectTransform trans = UItextGO.AddComponent<RectTransform>();
			trans.localPosition = new Vector2(0, StartHeight + i * BufferedHeight);

			Text text = UItextGO.AddComponent<Text>();
			text.text = Levels[i][0];
			text.font = textFont;
			text.fontSize = 12;
			text.color = Color.black;
			text.alignment = TextAnchor.MiddleCenter;
			text.transform.localScale = new Vector3(1, 1, 1);
			text.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);
			if (i == 0)
			{
				myBufferedHeight = text.transform.position.y;
			}
			else if (i == 1)
			{
				myBufferedHeight -= text.transform.localPosition.y;
			}
		}
		Selected = 0;
	}

	// Update is called once per frame
	void Update () {
		if ((resetP1 && Input.GetAxis("P1Vertical") > .5 || resetP2 && Input.GetAxis("P2Vertical") > .5) && Selected > 0)
		{
			resetP1 = !(Input.GetAxis("P1Vertical") > .5);
			resetP2 = !(Input.GetAxis("P2Vertical") > .5);
			LevelsContainer.transform.position = new Vector3(LevelsContainer.transform.position.x, LevelsContainer.transform.position.y - myBufferedHeight, LevelsContainer.transform.position.z);
			Selected -= 1;
		}
		if ((resetP1 && Input.GetAxis("P1Vertical") < -.5 || resetP2 && Input.GetAxis("P2Vertical") < -.5) && Selected < Levels.Count - 1)
		{
			resetP1 = !(Input.GetAxis("P1Vertical") < -.5);
			resetP2 = !(Input.GetAxis("P2Vertical") < -.5);
			LevelsContainer.transform.position = new Vector3(LevelsContainer.transform.position.x, LevelsContainer.transform.position.y + myBufferedHeight, LevelsContainer.transform.position.z);
			Selected += 1;
		}

		if (Input.GetAxis("P1Vertical") > -.5 && Input.GetAxis("P1Vertical") < .5)
		{
			resetP1 = true;
		}
		if (Input.GetAxis("P2Vertical") > -.5 && Input.GetAxis("P2Vertical") < .5)
		{
			resetP2 = true;
		}

		if (Input.GetAxis("P1Jump") > .5 || Input.GetButton("P1Jump") || Input.GetAxis("P2Jump") > .5 || Input.GetButton("P2Jump"))
		{
			if (startTime + 1 < Time.time)
			{
				SceneManager.LoadSceneAsync(Levels[Selected][1]);
			}
		}

		if (Input.GetButton("P1Back") || Input.GetButton("P2Back") || Input.GetButton("Back"))
		{
			SceneController.GetComponent<OptoinsSceenController>().StopLevelSelect();
			SceneController.GetComponent<OptoinsSceenController>().StartMain();
		}
	}
}
