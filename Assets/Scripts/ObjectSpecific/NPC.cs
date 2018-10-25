﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class NPC : MonoBehaviour {
    public string[] dialogue;
    public GameObject panel;
    public Text text;
    bool isSpeaking = false;
    int line = 0;
    Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.InteractAction = onInteract;
    }

    void onInteract(GameObject player)
    {
        Speak();
    }

    void Speak()
    {
        if (!isSpeaking)
        {
            isSpeaking = true;
            panel.SetActive(true);
            text.gameObject.SetActive(true);
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
    }
}