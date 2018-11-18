using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateMachine : MonoBehaviour {
    Dictionary<TutorialState, TutorialNode> states;

	// Use this for initialization
	void Awake () {
        states = new Dictionary<TutorialState, TutorialNode>();

        states.Add(TutorialState.Jump, new TutorialNode());
        states[TutorialState.Jump].Value = false;
        states[TutorialState.Jump].Text = "If you see an arrow appear next to an object, you can press A to jump.";

        states.Add(TutorialState.InteractOn, new TutorialNode());
        states[TutorialState.InteractOn].Value = false;
        states[TutorialState.InteractOn].Text = "<color=\"#9999ffff\">ON BOT</color>: Press LEFT TRIGGER to interact with objects.  You can only turn machines <color=\"#9999ffff\">ON</color>";

        states.Add(TutorialState.InteractOff, new TutorialNode());
        states[TutorialState.InteractOff].Value = false;
        states[TutorialState.InteractOff].Text = "<color=\"#ff9999ff\">OFF BOT</color>: Press RIGHT TRIGGER to interact with objects.  You can only turn machines <color=\"#ff9999ff\">OFF</color>";

        states.Add(TutorialState.SpeakToNPC, new TutorialNode());
        states[TutorialState.SpeakToNPC].Value = false;
        states[TutorialState.SpeakToNPC].Text = "If you an NPC, interact with them to talk to them.";

        states.Add(TutorialState.PickUpItem, new TutorialNode());
        states[TutorialState.PickUpItem].Value = false;
        states[TutorialState.PickUpItem].Text = "Some objects can be picked up.  Press B to pick it up, then press B again to drop it.";
    }
	
	public bool GetStateValue(TutorialState state)
    {
        return states[state].Value;
    }

    public string GetStateText(TutorialState state)
    {
        return states[state].Text;
    }

    public void SetStateValue(TutorialState state, bool value)
    {
        states[state].Value = value;
    }
}


public class TutorialNode
{
    public string Text { get; set; }
    public bool Value { get; set; }
}
