using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour {
    public GameObject dialoguePanel;
    public Text dialogueText;

    // Use this for initialization
    void Start ()
    {
        DismissDialogue();
    }

    public void ReceiveText(string text)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;
        dialogueText.text += "\r\n<color=\"orange\">Press Y to dismiss</color>";
    }

    public void DismissDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }
}
