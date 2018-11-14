using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUI;

public class HUDController : MonoBehaviour {
    HtmlDocument document;
    HtmlDivElement main;
    HtmlDivElement textPanel;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
        document = UI.document;
        main = (HtmlDivElement)document.getElementById("main");
        textPanel = (HtmlDivElement)document.getElementById("textPanel");
        DismissDialogue();
    }
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.Input.GetButtonUp("P1Dismiss") || UnityEngine.Input.GetButtonUp("P2Dismiss"))
            DismissDialogue();
	}

    public void ReceiveText(string text)
    {
        print(main);
        print(text);
        main.setAttribute("display", "inline-block");
        textPanel.innerHTML = text;
    }

    void DismissDialogue()
    {
        main.setAttribute("display", "none");
    }
}
