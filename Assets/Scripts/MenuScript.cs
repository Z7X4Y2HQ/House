using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject continueButtonText;
    private SwapCharacter SwapCharacter;
    public static int isFirstSave = 0;
    private string savedPlayer;
    private GameObject currentPlayer;
    public GameObject LoadingScreen;
    public Slider slider;
    private Animator menuExpand;
    private Animator settingText;
    private Animator bookText;
    private Animator SettingsExpanded;
    private Animator ChaptersExpanded;
    private Animator warningScreen;
    private GameObject menu;
    private GameObject titleScreen;
    private bool onTitleScreen;
    public static bool clickedOnSettings;
    public static bool clickedOnChapters;

    private void Awake()
    {
        menu = GameObject.Find("Menu");
        settingText = GameObject.Find("SettingText").GetComponent<Animator>();
        bookText = GameObject.Find("BookText").GetComponent<Animator>();
        titleScreen = GameObject.Find("TitleScreen");
        menu.SetActive(false);
        titleScreen.SetActive(false);
        warningScreen = GameObject.Find("WarningScreen").GetComponent<Animator>();
        isFirstSave = PlayerPrefs.GetInt("isFirstSave");
        if (isFirstSave == 0)
        {
            continueButtonText.GetComponent<TextMeshProUGUI>().text = "How can you continue when you haven't even played yet?";
        }
        else
        {
            continueButtonText.GetComponent<TextMeshProUGUI>().text = "Continue";
        }
        StartCoroutine(WarningScreen());
    }

    private void Update()
    {
        Debug.Log(Input.anyKeyDown);
        Debug.Log("clickedOnChapters " + clickedOnChapters);
        Debug.Log("clickedOnSettings " + clickedOnSettings);
        if (onTitleScreen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onTitleScreen = false;
                titleScreen.GetComponent<Animator>().Play("TitleScreenFadeOut");
                StartCoroutine(titleScreenDisableDelay());
                menu.SetActive(true);
                menuExpand = menu.GetComponent<Animator>();
                SettingsExpanded = GameObject.Find("SettingsExpanded").GetComponent<Animator>();
                ChaptersExpanded = GameObject.Find("ChaptersExpanded").GetComponent<Animator>();
                SettingsExpanded.gameObject.SetActive(false);
                ChaptersExpanded.gameObject.SetActive(false);
            }
        }
    }
    public void NewGame()
    {
        StartCoroutine(LoadAsynchronously("Dream"));
        HandleProgress.currentChapter = 1;
        HandleProgress.currentChapterName = "Chapter One";
        HandleProgress.currentScene = "Chapter_one_first_dream";
        HandleProgress.firstPlaythrough = true;
        PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_home");
    }

    public void Continue()
    {
        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetString("lastActiveScene")));
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    IEnumerator LoadAsynchronously(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Cursor.visible = false;
        savedPlayer = PlayerPrefs.GetString("currentCharacter");
        SwapCharacter = FindObjectOfType<CharacterController>().GetComponent<SwapCharacter>();

        if (savedPlayer == "Takahashi_Summer_home")
        {
            // SwapCharacter.Swap(SwapCharacter.Takahashi_Summer_home);
            currentPlayer = GameObject.FindWithTag("Takahashi_Summer_home");
        }
        else if (savedPlayer == "Takahashi_Summer_school")
        {
            if (SwapCharacter.gameObject.tag != "Takahashi_Summer_school")
            {
                SwapCharacter.Swap(SwapCharacter.Takahashi_Summer_school);
            }
            currentPlayer = GameObject.FindWithTag("Takahashi_Summer_school");
        }

        Vector3 newPlayerPosition = new Vector3(PlayerPrefs.GetFloat("playerPositionX"), PlayerPrefs.GetFloat("playerPositionY"), PlayerPrefs.GetFloat("playerPositionZ"));
        Quaternion newPlayerRotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("playerRotationY"), 0);

        currentPlayer.transform.position = newPlayerPosition;
        currentPlayer.transform.eulerAngles = newPlayerRotation.eulerAngles;

        HandleProgress.currentChapter = PlayerPrefs.GetInt("currentChapter");
        HandleProgress.currentScene = PlayerPrefs.GetString("currentScene");
        HandleProgress.currentObjectiveIndex = PlayerPrefs.GetInt("currentObjectiveIndex");
        HandleProgress.tutorialComplete = intToBool(PlayerPrefs.GetInt("tutorialComplete"));
        HandleProgress.pickedUpPhone = intToBool(PlayerPrefs.GetInt("pickedUpPhone"));
        HandleProgress.pickedUpKnife = intToBool(PlayerPrefs.GetInt("pickedUpKnife"));
        HandleProgress.readyForSchool = intToBool(PlayerPrefs.GetInt("readyForSchool"));



        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void Chapters()
    {
        clickedOnChapters = true;
        if (clickedOnSettings)
        {
            clickedOnSettings = false;
            bookText.Play("BookMoveUp");
            settingText.Play("SettingsMoveDown");
            StartCoroutine(DelayExpandedMenuTextEnable());
        }
        else if (!clickedOnSettings)
        {
            menuExpand.Play("OptionExpandedNewSlideIn");
            bookText.Play("BookMoveUp");
            StartCoroutine(DelayExpandedMenuText());
        }
    }

    public void Settings()
    {
        clickedOnSettings = true;
        if (clickedOnChapters)
        {
            clickedOnChapters = false;
            bookText.Play("BookMoveDown");
            settingText.Play("SettingsMoveUp");
            StartCoroutine(DelayExpandedMenuTextEnable());
        }
        else if (!clickedOnChapters)
        {
            menuExpand.Play("OptionExpandedNewSlideIn");
            settingText.Play("SettingsMoveUp");
            StartCoroutine(DelayExpandedMenuText());
        }
    }

    public void Back()
    {
        if (clickedOnSettings)
        {
            clickedOnSettings = false;
            settingText.Play("SettingsMoveDown");
            SettingsExpanded.Play("SettingsOptionsTextFadeOut");
        }
        else if (clickedOnChapters)
        {
            clickedOnChapters = false;
            bookText.Play("BookMoveDown");
            ChaptersExpanded.Play("ChaptersOptionsTextFadeOut");
        }
        menuExpand.Play("OptionExpandedNewSlideOut");
    }

    private IEnumerator DelayExpandedMenuText()
    {
        yield return new WaitForSeconds(0.4f);
        if (clickedOnSettings)
        {
            SettingsExpanded.gameObject.SetActive(true);
            SettingsExpanded.Play("SettingsOptionsTextFadeIn");
        }
        else if (clickedOnChapters)
        {
            ChaptersExpanded.gameObject.SetActive(true);
            ChaptersExpanded.Play("ChaptersOptionsTextFadeIn");
        }

    }

    private IEnumerator DelayExpandedMenuTextEnable()
    {
        if (clickedOnChapters)
        {
            ChaptersExpanded.gameObject.SetActive(true);
            ChaptersExpanded.Play("ChaptersOptionsTextFadeIn");
            SettingsExpanded.Play("SettingsOptionsTextFadeOut");
            yield return new WaitForSeconds(0.33f);
            SettingsExpanded.gameObject.SetActive(false);
        }
        else if (clickedOnSettings)
        {
            SettingsExpanded.gameObject.SetActive(true);
            SettingsExpanded.Play("SettingsOptionsTextFadeIn");
            ChaptersExpanded.Play("ChaptersOptionsTextFadeOut");
            yield return new WaitForSeconds(0.33f);
            ChaptersExpanded.gameObject.SetActive(false);
        }
    }
    private IEnumerator WarningScreen()
    {
        yield return new WaitForSeconds(6f);
        warningScreen.Play("WarningScreenFadeOut");
        yield return new WaitForSeconds(1f);
        warningScreen.gameObject.SetActive(false);
        titleScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        onTitleScreen = true;
    }

    private IEnumerator titleScreenDisableDelay()
    {
        yield return new WaitForSeconds(1f);
        titleScreen.SetActive(false);
        menu.GetComponent<Animator>().Play("MainMenuFadeIn");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
