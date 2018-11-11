using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PowerUI;

public class MainMenuScript : MonoBehaviour {
    HtmlDocument document;
    private List<string[]> levels;
    float cooldown = 0.25f;
    float lastInteracted = 0f;
    int focusIndex = 0;
    int maxFocus = 3;

    // Use this for initialization
    void Start()
    {
        document = UI.document;

        HtmlButtonElement btnStart = (HtmlButtonElement)document.getElementById("btnStart");
        var btnLeaderboards = document.getElementById("btnLeaderboards");
        var btnCredits = document.getElementById("btnCredits");
        var btnExit = document.getElementById("btnExit");

        PopulateLevels();

        var levelSelect = document.getElementById("levelSelect");
        var btnLvlSelectBack = document.getElementById("lvlSelectBack");

        int i = 0;
        for (i = 0; i < levels.Count; i++)
        {
            var button = document.createElement("button");
            button.innerText = levels[i][0];
            button.onclick = delegate (MouseEvent mouseEvent) { SceneManager.LoadScene(levels[i][1]); };
            button.setAttribute("tabindex", i.ToString());
            levelSelect.insertBefore(button, btnLvlSelectBack);
        }
        levelSelect.style.display = "none";
        btnLvlSelectBack.setAttribute("tabindex", i.ToString());

        btnStart.onmousedown = btnStart_Click;
        btnStart.focus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastInteracted > cooldown)
        {
            if (focusIndex > 0 && (UnityEngine.Input.GetAxis("P1Vertical") > 0.5 || UnityEngine.Input.GetAxis("P2Vertical") > 0.5))
            {
                document.MoveFocusUp();
                lastInteracted = Time.time;
                focusIndex--;
            }
            if (focusIndex < maxFocus && (UnityEngine.Input.GetAxis("P1Vertical") < -0.5 || UnityEngine.Input.GetAxis("P2Vertical") < -0.5))
            {
                document.MoveFocusDown();
                lastInteracted = Time.time;
                focusIndex++;
            }
            if (UnityEngine.Input.GetButtonUp("P1Jump") ||
                UnityEngine.Input.GetButtonUp("P1Jump") ||
                UnityEngine.Input.GetKeyUp(KeyCode.Return))
            {
                document.ClickFocused();
                lastInteracted = Time.time;
            }
        }
    }

    private void PopulateLevels()
    {
        levels = new List<string[]>();
        levels.Add(new string[] { "Section 1", "Scenes/Progression chunks/Section 1" });
        levels.Add(new string[] { "PressurePlateLevel", "Scenes/Level Ideas/PressurePlateLevel" });
    }

    void btnStart_Click(MouseEvent mouseEvent)
    {
        var levelSelect = document.getElementById("levelSelect");
        levelSelect.style.display = "flex";
        var mainMenu = document.getElementById("mainMenu");
        mainMenu.style.display = "none";
        focusIndex = 0;
        maxFocus = levels.Count;
    }

    void btnLvlSelectBack_Click(MouseEvent mouseEvent)
    {
        var levelSelect = document.getElementById("levelSelect");
        levelSelect.style.display = "none";
        var mainMenu = document.getElementById("mainMenu");
        mainMenu.style.display = "flex";
        focusIndex = 0;
        maxFocus = 3;
    }
}
