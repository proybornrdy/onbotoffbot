using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Text TIME_TEXT;
	public Text GAME_END_STATUS;

	void Start()
	{
	}

	void Update()
	{
		int minutes = ((int)LevelController.getTime()) / 60;
		int seconds = ((int)LevelController.getTime()) % 60;
        TIME_TEXT.text = "" + minutes.ToString("D2") + ":" + seconds.ToString("D2");

		if (!LevelController.gameGoing())
		{
			GAME_END_STATUS.text = LevelController.getReason();
		}
	}
}