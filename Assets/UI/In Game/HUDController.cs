using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour {
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject xboxButtonImg;
    GameObject dialogueImg;

    // Use this for initialization
    void Start ()
    {
        DismissDialogue();
    }

    public void ReceiveText(string text)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;
        dialogueText.text += "\r\n<color=\"orange\">Press     to dismiss</color>";
        xboxButtonImg.SetActive(true);
    }

    public void ReceiveButtons(List<XboxButton> buttons)
    {
        DismissButtons();
        if (buttons != null)
        {
            foreach (XboxButton button in buttons)
            {
                dialogueImg = Instantiate(xboxButtonImg, dialogueText.transform);

                RawImage buttonImage = dialogueImg.GetComponent<RawImage>();
                RectTransform buttonTransform = dialogueImg.GetComponent<RectTransform>();

                buttonImage.texture = Resources.Load<Texture2D>(button.imgPath);
                buttonTransform.anchoredPosition = button.imgPosition;
                buttonTransform.sizeDelta = button.imgSize;
            }
        }
    }

    public void DismissButtons()
    {
        foreach (Transform child in dialogueText.transform)
        {
            Destroy(child.gameObject);
        }        
    }


    public void DismissDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);   
        DismissButtons();
    }
}
