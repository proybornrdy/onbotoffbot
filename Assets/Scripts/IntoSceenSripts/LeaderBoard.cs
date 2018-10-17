using System;
using UnityEngine;

public class LeaderBoard : MonoBehaviour {
	private string leadersInfo = "frist\tsecond\t0\nfrist\tsecond\t0";
	public GameObject LeaderBoardEntriesContainer;
	public Transform LeaderBoardEntry;

	public float StartHeight;
	public float BufferedHeight;

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
			Debug.Log(leaderValues);
			Debug.Log(leaderValues[0]);
			Debug.Log(leaderValues[1]);
			Debug.Log(leaderValues[2]);
			lbes.GetComponent<LeaderBoardEntry>().
					setInfo(leaderValues[0], leaderValues[1], Convert.ToInt32(leaderValues[2]));
		}	
	}
}
