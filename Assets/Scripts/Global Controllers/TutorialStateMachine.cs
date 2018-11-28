using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateMachine : MonoBehaviour {
    Dictionary<TutorialState, TutorialNode> states;

    // Use this for initialization
    void Awake() {
        states = new Dictionary<TutorialState, TutorialNode>();


        states.Add(TutorialState.Jump, new TutorialNode());
        states[TutorialState.Jump].Value = false;
        states[TutorialState.Jump].Text = "If you see an arrow appear next to an object, you can press    \n     to jump.";
        states[TutorialState.Jump].Buttons = new List<XboxButton>();
        states[TutorialState.Jump].Buttons.Add(new XboxButton("Xbox Buttons/xbox_a", new Vector2(-182F, 1F), new Vector2(15F, 15F)));
        states[TutorialState.Jump].Buttons.Add(new XboxButton("Xbox Buttons/xbox_y", new Vector2(-145F, -15F), new Vector2(15F, 15F)));


        states.Add(TutorialState.InteractOn, new TutorialNode());
        states[TutorialState.InteractOn].Value = false;
        states[TutorialState.InteractOn].Text = "<color=\"#9999ffff\">ON BOT</color>: Press LEFT BUMPER (         ) to interact with objects. You can only turn machines <color=\"#9999ffff\">ON</color>";
        states[TutorialState.InteractOn].Buttons = new List<XboxButton>();
        states[TutorialState.InteractOn].Buttons.Add(new XboxButton("Xbox Buttons/xbox_lb", new Vector2(35F, 17F), new Vector2(30F, 15F)));
        states[TutorialState.InteractOn].Buttons.Add(new XboxButton("Xbox Buttons/xbox_y", new Vector2(-145F, -15F), new Vector2(15F, 15F)));

        states.Add(TutorialState.InteractOff, new TutorialNode());
        states[TutorialState.InteractOff].Value = false;
        states[TutorialState.InteractOff].Text = "<color=\"#ff9999ff\">OFF BOT</color>: Press RIGHT BUMPER (         ) to interact with objects. You can only turn machines <color=\"#ff9999ff\">OFF</color>";
        states[TutorialState.InteractOff].Buttons = new List<XboxButton>();
        states[TutorialState.InteractOff].Buttons.Add(new XboxButton("Xbox Buttons/xbox_rb", new Vector2(50F, 17F), new Vector2(30F, 15F)));
        states[TutorialState.InteractOff].Buttons.Add(new XboxButton("Xbox Buttons/xbox_y", new Vector2(-145F, -15F), new Vector2(15F, 15F)));

        states.Add(TutorialState.SpeakToNPC, new TutorialNode());
        states[TutorialState.SpeakToNPC].Value = false;
        states[TutorialState.SpeakToNPC].Text = "Interact with NPCs to talk to them.\r\n";
        states[TutorialState.SpeakToNPC].Buttons = new List<XboxButton>();
        states[TutorialState.SpeakToNPC].Buttons.Add(new XboxButton("Xbox Buttons/xbox_y", new Vector2(-145F, -15F), new Vector2(15F, 15F)));

        states.Add(TutorialState.PickUpItem, new TutorialNode());
        states[TutorialState.PickUpItem].Value = false;
        states[TutorialState.PickUpItem].Text = "Some objects can be picked up. Press     to pick it up, then press     again to drop it.";
        states[TutorialState.PickUpItem].Buttons = new List<XboxButton>();
        states[TutorialState.PickUpItem].Buttons.Add(new XboxButton("Xbox Buttons/xbox_b", new Vector2(53F, 17F), new Vector2(15F, 15F)));
        states[TutorialState.PickUpItem].Buttons.Add(new XboxButton("Xbox Buttons/xbox_b", new Vector2(-146F, 1F), new Vector2(15F, 15F)));
        states[TutorialState.PickUpItem].Buttons.Add(new XboxButton("Xbox Buttons/xbox_y", new Vector2(-145F, -15F), new Vector2(15F, 15F)));
    }

    public bool GetStateValue(TutorialState state) {
        return states[state].Value;
    }

    public string GetStateText(TutorialState state) {
        return states[state].Text;
    }

    public List<XboxButton> GetStateButtons(TutorialState state) {
        return states[state].Buttons;
    }

    public void SetStateValue(TutorialState state, bool value) {
        states[state].Value = value;
    }
}


public class TutorialNode {
    public string Text { get; set; }
    public bool Value { get; set; }
    public List<XboxButton> Buttons { get; set; }
}
