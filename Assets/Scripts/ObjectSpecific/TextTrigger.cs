using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

    public GameObject panel;
    public GameObject text;

	public void TurnOn()
    {
        panel.SetActive(true);
        text.SetActive(true);
    }

    public void TurnOff()
    {
        panel.SetActive(false);
        text.SetActive(false);
    }
}
