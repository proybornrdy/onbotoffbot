using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRunner : MonoBehaviour {

	public float scrollSpeed;
	public float scrollStart;
	public float scrollRestart;

	// Use this for initialization
	void Start () {
		Restart_Scroll();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += (new Vector3(0, 1, 0) * scrollSpeed);

		if (this.transform.position.y > scrollRestart)
		{
			Restart_Scroll();
		}
	}

	public void Restart_Scroll()
	{
		this.transform.position = new Vector3(0.0f, scrollStart, 5f);
	}
}
