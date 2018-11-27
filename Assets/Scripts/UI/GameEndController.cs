using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour {

	public Text t;
	public InputField i_feild;

	public Button skip;
	public Button submit;

	// Use this for initialization
	void Start () {
		int minutes = ((int)LevelController.getTime()) / 60;
		int seconds = ((int)LevelController.getTime()) % 60;
		t.text = "" + minutes.ToString("D2") + ":" + seconds.ToString("D2");

		skip.onClick.AddListener(skipClick);
		submit.onClick.AddListener(submitClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void skipClick() {
		SceneManager.LoadScene("MainMenu");
	}

	void submitClick() {
		if (i_feild.text.Length == 0) {
			return;
		}
		string s = "{\"team_name\": \"" + i_feild.text + "\", \"score\": " + LevelController.getTime() + "}";
		Debug.Log(s);
		StartCoroutine(PostRequest("https://www.mdshulman.com/game/onbotoffbot/scores", s));
	}

	IEnumerator PostRequest(string url, string json)
	{
		var uwr = new UnityWebRequest(url, "PUT");
		byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
		uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
		uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		uwr.SetRequestHeader("Content-Type", "application/json");

		//Send the request then wait here until it returns
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uwr.error);
		}
		SceneManager.LoadScene("MainMenu");
	}
}
