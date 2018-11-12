using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PowerUI;

public class MainMenuScript : MonoBehaviour {
    HtmlDocument document;
    float cooldown = 0.25f;
    float lastInteracted = 0f;
    bool selectingPlayers = false;
    Player? player1Selection;
    Player? player2Selection;
    private List<string[]> levels;

    Dom.Element mainMenu;
    HtmlButtonElement btnStart;
    HtmlButtonElement btnLeaderboards;
    HtmlButtonElement btnCredits;
    HtmlButtonElement btnExit;

    Dom.Element playerSelect;
    Dom.Element player1Selector;
    Dom.Element player2Selector;
    HtmlButtonElement btnPlayerSelectDone;
    HtmlButtonElement btnPlayerSelectBack;

    Dom.Element levelSelect;
    HtmlButtonElement[] levelButtons;
    HtmlButtonElement btnLevelSelectBack;

    // Use this for initialization
    void Start()
    {
        SetupElements();
        LoadMainMenu();
    }

    void SetupElements()
    {
        document = UI.document;

        mainMenu = document.getElementById("mainMenu");
        btnStart = (HtmlButtonElement)document.getElementById("btnStart");
        btnStart.onclick = btnStart_Click;
        btnLeaderboards = (HtmlButtonElement)document.getElementById("btnLeaderboards");
        btnLeaderboards.onclick = btnLeaderboards_Click;
        btnCredits = (HtmlButtonElement)document.getElementById("btnCredits");
        btnCredits.onclick = btnCredits_Click;
        btnExit = (HtmlButtonElement)document.getElementById("btnExit");
        btnExit.onclick = btnExit_Click;

        playerSelect = document.getElementById("playerSelect");
        player1Selector = document.getElementById("player1Selector");
        player2Selector = document.getElementById("player2Selector");
        btnPlayerSelectDone = (HtmlButtonElement)document.getElementById("btnPlayerSelectDone");
        btnPlayerSelectDone.onclick = btnPlayerSelectDone_Click;
        btnPlayerSelectBack = (HtmlButtonElement)document.getElementById("btnPlayerSelectBack");

        PopulateLevels();

        levelSelect = document.getElementById("levelSelect");
        btnLevelSelectBack = (HtmlButtonElement)document.getElementById("btnLevelSelectBack");
        levelButtons = new HtmlButtonElement[levels.Count];
        btnLevelSelectBack.tabIndex = -1;
        btnLevelSelectBack.onclick = btnLvlSelectBack_Click;
        for (int i = 0; i < levels.Count; i++)
        {
            var button = (HtmlButtonElement)document.createElement("button");
            button.innerText = levels[i][0];
            button.id = "btnLevel" + i;
            var index = i;
            button.onclick = delegate (MouseEvent mouseEvent) { SceneManager.LoadScene(levels[index][1]); };
            button.tabIndex = -1;
            levelSelect.insertBefore(button, btnLevelSelectBack);
            levelButtons[i] = button;
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void btnStart_Click(MouseEvent mouseEvent)
    {
        LoadPlayerSelect();
    }

    void btnLeaderboards_Click(MouseEvent mouseEvent)
    {
        LoadLeaderboards();
    }

    void btnCredits_Click(MouseEvent mouseEvent)
    {
        LoadCredits();
    }

    void btnExit_Click(MouseEvent mouseEvent)
    {
        Application.Quit();
    }

    void btnLvlSelectBack_Click(MouseEvent mouseEvent)
    {
        LoadMainMenu();
    }

    void btnPlayerSelectDone_Click(MouseEvent mouseEvent)
    {
        if (player1Selection.HasValue && player2Selection.HasValue && player1Selection != player2Selection)
        {
            PlayerInputTranslator.SetP1P2(player1Selection.Value, player2Selection.Value);
            selectingPlayers = false;
            LoadLevelSelect();
        }
    }

    private void HandleInput()
    {
        if (Time.time - lastInteracted > cooldown)
        {
            if (UnityEngine.Input.GetAxis("P1Vertical") > 0.5 || UnityEngine.Input.GetAxis("P2Vertical") > 0.5)
            {
                document.TabPrevious();
                lastInteracted = Time.time;
            }
            if (UnityEngine.Input.GetAxis("P1Vertical") < -0.5 || UnityEngine.Input.GetAxis("P2Vertical") < -0.5)
            {
                document.TabNext();
                lastInteracted = Time.time;
            }
            if (UnityEngine.Input.GetButtonUp("P1Jump") ||
                UnityEngine.Input.GetButtonUp("P2Jump") ||
                UnityEngine.Input.GetKeyUp(KeyCode.Return))
            {
                document.ClickFocused();
                lastInteracted = Time.time;
            }

            if (selectingPlayers)
            {
                if (UnityEngine.Input.GetAxis("P1Horizontal") < -0.5)
                {
                    if (player1Selection == Player.OFF)
                    {
                        player1Selector.style.left = "207";
                        player1Selection = null;
                    }
                    else
                    {
                        player1Selector.style.left = "107";
                        player1Selection = Player.ON;
                    }
                    lastInteracted = Time.time;
                    print(player1Selection);
                }
                if (UnityEngine.Input.GetAxis("P2Horizontal") < -0.5)
                {
                    if (player2Selection == Player.OFF)
                    {
                        player2Selector.style.left = "207";
                        player2Selection = null;
                    }
                    else
                    {
                        player2Selector.style.left = "107";
                        player2Selection = Player.ON;
                    }
                    lastInteracted = Time.time;
                    print(player2Selection);
                }
                if (UnityEngine.Input.GetAxis("P1Horizontal") > 0.5)
                {
                    if (player1Selection == Player.ON)
                    {
                        player1Selector.style.left = "207";
                        player1Selection = null;
                    }
                    else
                    {
                        player1Selector.style.left = "307";
                        player1Selection = Player.OFF;
                    }
                    lastInteracted = Time.time;
                    print(player1Selection);
                }
                if (UnityEngine.Input.GetAxis("P2Horizontal") > 0.5)
                {
                    if (player2Selection == Player.ON)
                    {
                        player2Selector.style.left = "207";
                        player2Selection = null;
                    }
                    else
                    {
                        player2Selector.style.left = "307";
                        player2Selection = Player.OFF;
                    }
                    print(player2Selection);
                    lastInteracted = Time.time;
                }
            }
        }

    }

    private void UnloadAllMenus()
    {
        UnloadMainMenu();
        UnloadPlayerSelect();
        UnloadLevelSelect();
        UnloadLeaderboards();
        UnloadCredits();
        selectingPlayers = false;
    }

    private void UnloadMainMenu()
    {
        btnStart.tabIndex = -1;
        btnLeaderboards.tabIndex = -1;
        btnCredits.tabIndex = -1;
        btnExit.tabIndex = -1;
        mainMenu.style.display = "none";
    }

    private void UnloadPlayerSelect()
    {
        btnPlayerSelectDone.tabIndex = -1;
        btnPlayerSelectBack.tabIndex = -1;
        playerSelect.style.display = "none";
    }

    private void UnloadLevelSelect()
    {
        foreach (var b in levelButtons) b.tabIndex = -1;
        btnLevelSelectBack.tabIndex = -1;
        levelSelect.style.display = "none";
    }

    private void UnloadLeaderboards()
    {
        ;
    }

    private void UnloadCredits()
    {
        ;
    }

    private void LoadMainMenu()
    {
        UnloadAllMenus();
        btnStart.tabIndex = 0;
        btnLeaderboards.tabIndex = 0;
        btnCredits.tabIndex = 0;
        btnExit.tabIndex = 0;
        mainMenu.style.display = "flex";
    }

    private void LoadPlayerSelect()
    {
        UnloadAllMenus();
        selectingPlayers = true;
        btnPlayerSelectDone.tabIndex = 0;
        btnPlayerSelectBack.tabIndex = 0;
        playerSelect.style.display = "flex";
    }

    private void LoadLevelSelect()
    {
        UnloadAllMenus();
        foreach (var b in levelButtons) b.tabIndex = 0;
        btnLevelSelectBack.tabIndex = 0;
        levelSelect.style.display = "flex";
    }

    private void LoadLeaderboards()
    {
        UnloadAllMenus();
        ;
    }

    private void LoadCredits()
    {
        UnloadAllMenus();
        ;
    }

    private void PopulateLevels()
    {
        levels = new List<string[]>();
        levels.Add(new string[] { "Section 1", "Scenes/Progression chunks/Section 1" });
        levels.Add(new string[] { "PressurePlateLevel", "Scenes/Level Ideas/PressurePlateLevel" });
    }
}
