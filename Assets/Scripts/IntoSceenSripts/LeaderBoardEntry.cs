using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardEntry : MonoBehaviour {
	public Text OnPlayerText;
	public Text OffPlayerText;
	public Text TimeText;

	public void setInfo(string OnPlayer, string OffPlayer, int time)
	{
		OnPlayerText.text = OnPlayer;
		OffPlayerText.text = OffPlayer;
		TimeText.text = "" + (time / 60).ToString("D2") + ":" + (time % 60).ToString("D2"); ;
	}
}
