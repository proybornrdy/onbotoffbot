using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

    public GameObject panel;
    public GameObject text;

	public void TurnOn()
    {
        TurnAllDialogueOff();
        panel.SetActive(true);
        text.SetActive(true);
    }

    public void TurnOff()
    {
        panel.SetActive(false);
        text.SetActive(false);
    }

    private void TurnAllDialogueOff()
    {
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
