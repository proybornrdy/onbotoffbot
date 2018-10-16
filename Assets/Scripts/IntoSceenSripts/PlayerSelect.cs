using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
	public GameObject P1Select;
	public GameObject P2Select;
	public float CenterPostion;
	public float OnPosition;
	public float OffPosition;
	public string next_sceen_name;

	public void SetUp()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame()
	{
		SceneManager.LoadSceneAsync(next_sceen_name);
	}

	private bool CanMoveLeft(GameObject selectionObject)
	{
		return selectionObject.transform.position.x > OnPosition;
	}

	private bool CanMoveRight(GameObject selectionObject)
	{
		return selectionObject.transform.position.x < OffPosition;
	}
}
