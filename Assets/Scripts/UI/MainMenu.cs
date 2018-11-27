using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //main
    public GameObject pnlMain;
    public Button btnContinue;
    public Button btnStart;
    public Button btnLeaderboards;
    public Button btnCredits;
    public Button btnQuit;

    //player select
    public GameObject pnlPlayerSelect;
    public Slider player1Selector;
    public Slider player2Selector;
    public Button btnPlayerSelectDone;
    public Button btnPlayerSelectBack;

    //level select
    public GameObject pnlLevelSelect;
    public GameObject scrollViewContent;
    public Button btnLevelSelectBack;

    //leaderboard
    public GameObject pnlLeaderboard;
    public Button btnLeaderboardBack;

    //credits
    public GameObject pnlCredits;
    public Button btnCreditsBack;

    public EventSystem eventSystem;
    public Button buttonPrefab;
    public Image gear0;
    public Image gear1;
    public Image gear2;
    public Image gear3;
    public Image gear4;
    public Image gear5;
    public Image gear6;
    public Image gear7;

    private bool newGame = true;

    bool selectingPlayers = false;

    // Use this for initialization
    void Start () {
        ActivateAllPanels();
        SetUpButtons();
        ShowMain();

        StartCoroutine("Rotate", new MyPair<Transform, float>(gear0.transform, 3f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear1.transform, 7f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear2.transform, -5f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear3.transform, 15f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear4.transform, -15f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear5.transform, -15f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear6.transform, 15f));
        StartCoroutine("Rotate", new MyPair<Transform, float>(gear7.transform, -15f));
    }

    // Update is called once per frame
    void Update()
    {
        if (selectingPlayers)
        {
            if (Input.GetAxis("P1Horizontal") < -0.5)
            {
                player1Selector.value -= 0.5f;
            }
            else if (Input.GetAxis("P1Horizontal") > 0.5)
            {
                player1Selector.value += 0.5f;
            }
            if (Input.GetAxis("P2Horizontal") < -0.5)
            {
                player2Selector.value -= 0.5f;
            }
            else if (Input.GetAxis("P2Horizontal") > 0.5)
            {
                player2Selector.value += 0.5f;
            }

            if (player1Selector.value != 0.5f && player2Selector.value != 0.5f && player1Selector.value != player2Selector.value)
            {
                btnPlayerSelectDone.interactable = true;
            }
            else
            {
                btnPlayerSelectDone.interactable = false;
            }
        }
    }

    void SetUpButtons()
    {
        //main
        btnContinue.onClick.AddListener(Continue);
        btnContinue.gameObject.SetActive(false);
        btnStart.onClick.AddListener(NewGame);
        btnLeaderboards.onClick.AddListener(Leaderboards);
        btnCredits.onClick.AddListener(Credits);
        btnQuit.onClick.AddListener(Quit);
        
        //player select
        btnPlayerSelectDone.onClick.AddListener(PlayerSelectDone);
        btnPlayerSelectBack.onClick.AddListener(PlayerSelectBack);
        
        //level select
        SetUpLevelButtons();
        btnLevelSelectBack.onClick.AddListener(LevelSelectBack);

        //leaderboard
        btnLeaderboardBack.onClick.AddListener(LeaderboardBack);

        //credits
        btnCreditsBack.onClick.AddListener(CreditsBack);
    }

    void ActivateAllPanels()
    {
        pnlMain.SetActive(true);
        pnlPlayerSelect.SetActive(true);
        pnlLevelSelect.SetActive(true);
        pnlLeaderboard.SetActive(true);
        pnlCredits.SetActive(true);
    }

    void DeactivateAllPanels()
    {
        pnlMain.SetActive(false);
        pnlPlayerSelect.SetActive(false);
        pnlLevelSelect.SetActive(false);
        pnlLeaderboard.SetActive(false);
        pnlCredits.SetActive(false);

    }

    void ShowMain()
    {
        DeactivateAllPanels();
        pnlMain.SetActive(true);
        btnStart.Select();
    }

    void ShowPlayerSelect()
    {
        DeactivateAllPanels();
        pnlPlayerSelect.SetActive(true);
        btnPlayerSelectBack.Select();
        selectingPlayers = true;
    }

    void ShowLevelSelect()
    {
        DeactivateAllPanels();
        pnlLevelSelect.SetActive(true);
        scrollViewContent.GetComponentInChildren<Button>().Select();
    }

    void ShowLeaderboard()
    {
        DeactivateAllPanels();
        pnlLeaderboard.SetActive(true);
    }

    void ShowCredits()
    {
        DeactivateAllPanels();
        pnlCredits.SetActive(true);
    }

    void SetUpLevelButtons()
    {
        Button btnSection1 = Instantiate<Button>(buttonPrefab);
        btnSection1.GetComponentInChildren<Text>().text = "Section 1";
        btnSection1.onClick.AddListener(delegate ()
        {
            LevelController.startInStatic = 0;
            SceneManager.LoadScene("Scenes/Progression chunks/Section 1");
        });
        btnSection1.transform.parent = scrollViewContent.transform;
        btnSection1.transform.localPosition = new Vector3(166.5f, -50, 0);
        btnSection1.transform.localScale = new Vector3(1, 1, 1);

        Button btnSection2 = Instantiate<Button>(buttonPrefab);
        btnSection2.GetComponentInChildren<Text>().text = "Section 2";
        btnSection2.onClick.AddListener(delegate ()
        {
            LevelController.startInStatic = 6;
            SceneManager.LoadScene("Scenes/Progression chunks/Section 1");
        });
        btnSection2.transform.parent = scrollViewContent.transform;
        btnSection2.transform.localPosition = new Vector3(166.5f, -110, 0);
        btnSection2.transform.localScale = new Vector3(1, 1, 1);
    }

    public void NewGame()
    {
        ShowPlayerSelect();
    }

    public void Continue()
    {
        newGame = false;
        ;
    }

    public void Leaderboards()
    {
        ;
    }

    public void Credits()
    {
        ;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void PlayerSelectDone()
    {
        selectingPlayers = false;
        PlayerInputTranslator.SetP1P2(
            (Player)(Mathf.RoundToInt(player1Selector.value)),
            (Player)(Mathf.RoundToInt(player2Selector.value)));
        ShowLevelSelect();
    }

    public void PlayerSelectBack()
    {
        selectingPlayers = false;
        ShowMain();
    }

    public void LevelSelectBack()
    {
        ShowPlayerSelect();
    }

    public void LeaderboardBack()
    {
        ShowMain();
    }

    public void CreditsBack()
    {
        ShowMain();
    }


    IEnumerator Rotate(MyPair<Transform, float> input)
    {
        while (true)
        {
            input.First.Rotate(0, 0, 1 * input.Second * Time.deltaTime);
            yield return null;
        }
    }

}
