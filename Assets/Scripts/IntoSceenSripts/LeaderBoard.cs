﻿using System;
using UnityEngine;

public class LeaderBoard : MonoBehaviour {
	private string leadersInfo = "frist\tsecond\t0\nfrist\tsecond\t0";
	public GameObject LeaderBoardEntriesContainer;
	public Transform LeaderBoardEntry;

	public float StartHeight;
	public float BufferedHeight;

	public GameObject SceneController;

	void Update()
	{
		if (Input.GetButton("P1Back") || Input.GetButton("P2Back") || Input.GetButton("Back"))
		{
			SceneController.GetComponent<OptoinsSceenController>().StopLeaderBoard();
			SceneController.GetComponent<OptoinsSceenController>().StartMain();
		}
	}

	public void updateLeaderBoard()
	{
		// remove all old entries
		foreach (Transform child in LeaderBoardEntriesContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		string[] leaderEntries = leadersInfo.Split('\n');
		Debug.Log(leaderEntries[0]);
		for (int i = 0; i < leaderEntries.Length; i++)
		{
			Transform lbes = Instantiate(LeaderBoardEntry, new Vector3(0, StartHeight+i*BufferedHeight, 5), Quaternion.identity);
			lbes.SetParent(LeaderBoardEntriesContainer.transform);
			lbes.localScale = new Vector3(1, 1, 1);
			string[] leaderValues = leaderEntries[i].Split('\t');
			//lbes.GetComponent<LeaderBoardEntry>().setInfo(leaderValues[0], leaderValues[1], Convert.ToInt32(leaderValues[2]));
		}
	}
}
