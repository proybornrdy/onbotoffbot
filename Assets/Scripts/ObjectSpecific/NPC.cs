using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class NPC : MonoBehaviour {
    public string[] dialogue;
    public GameObject panel;
    public Text text;
    public GameObject flashingSign;
    bool isSpeaking = false;
    int line = 0;
    bool interacted = false;
    bool signOn = true;
    private float timer;
    public float flashSpeed = 1f; //seconds
    Interactable interactable;
    HUDController hudController;

    private void Start()
    {
        hudController = FindObjectOfType<HUDController>();
        hudController.DismissButtons();
        hudController.xboxButtonImg.SetActive(false);
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = onInteract;
        InvokeRepeating("FlashSign", 0.1f, flashSpeed);
    }

    void Update()
    {
        if (interacted)
        {
            CancelInvoke();
        }
            
    }

    void onInteract(GameObject player)
    {
        Speak();
        interacted = true;
        flashingSign.gameObject.SetActive(false);
    }

    void Speak()
    {
        if (!isSpeaking)
        {
            isSpeaking = true;
            panel.SetActive(true);
            text.gameObject.SetActive(true);
            continueButton.SetActive(true);
            text.text = dialogue[line];
            line++;
        }
        else if(line == dialogue.Length) Dismiss();
        else
        {
            text.text = dialogue[line];
            line++;
        }
    }

    void Dismiss()
    {
        isSpeaking = false;
        panel.SetActive(false);
        text.gameObject.SetActive(false);
        line = 0;
        continueButton.SetActive(false);
    }

    void FlashSign()
    {
        signOn = !signOn;
        flashingSign.gameObject.SetActive(signOn);
    }
}
