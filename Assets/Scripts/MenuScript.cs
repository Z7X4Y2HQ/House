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

    private Animator warningScreen;
    private GameObject menu;
    private GameObject titleScreen;
    private AudioSource BGMusic;
    private bool onTitleScreen;
    private Animator SettingsExpanded;
    private Animator ChaptersExpanded;
    private Animator VideoExpanded;
    private Animator AudioExpanded;
    private Animator GameplayExpanded;
    public static bool clickedOnSettings;
    public static bool clickedOnChapters;
    public static bool clickedOnVideo;
    public static bool clickedOnAudio;
    public static bool clickedOnGameplay;
    public static bool invertAxis;
    public GameObject audioSlider;
    private bool foundRes = false;
    public TMP_Text resLabelText;
    public Toggle FSTog, VSyncTog, InvertAxisTog;
    private int selectedRes;
    public List<Resolution> resolutions = new List<Resolution>();

    private void Awake()
    {
        menu = GameObject.Find("Menu");
        BGMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        settingText = GameObject.Find("SettingText").GetComponent<Animator>();
        bookText = GameObject.Find("BookText").GetComponent<Animator>();
        titleScreen = GameObject.Find("TitleScreen");
        menu.SetActive(false);
        titleScreen.SetActive(false);
        warningScreen = GameObject.Find("WarningScreen").GetComponent<Animator>();
        isFirstSave = PlayerPrefs.GetInt("isFirstSave");
        StartCoroutine(WarningScreen());
        BGMusic.volume = audioSlider.GetComponent<Slider>().value;
        InvertAxisTog.isOn = intToBool(PlayerPrefs.GetInt("InvertAxis"));
        invertAxis = intToBool(PlayerPrefs.GetInt("InvertAxis"));

    }

    private void Start()
    {
        FSTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            VSyncTog.isOn = false;
        }
        else
        {
            VSyncTog.isOn = true;
        }
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;
                UpdateResLabel();
            }
        }
    }

    private void Update()
    {
        Debug.Log("Settings " + clickedOnSettings);
        Debug.Log("Chapters " + clickedOnChapters);
        Debug.Log("Video " + clickedOnVideo);
        if (onTitleScreen)
        {
            if (Input.anyKeyDown)
            {
                onTitleScreen = false;
                titleScreen.GetComponent<Animator>().Play("TitleScreenFadeOut");
                StartCoroutine(titleScreenDisableDelay());
                menu.SetActive(true);
                menuExpand = menu.GetComponent<Animator>();
                SettingsExpanded = GameObject.Find("SettingsExpanded").GetComponent<Animator>();
                ChaptersExpanded = GameObject.Find("ChaptersExpanded").GetComponent<Animator>();
                VideoExpanded = GameObject.Find("VideoExpanded").GetComponent<Animator>();
                AudioExpanded = GameObject.Find("AudioExpanded").GetComponent<Animator>();
                GameplayExpanded = GameObject.Find("GameplayExpanded").GetComponent<Animator>();
                SettingsExpanded.gameObject.SetActive(false);
                ChaptersExpanded.gameObject.SetActive(false);
                VideoExpanded.gameObject.SetActive(false);
                AudioExpanded.gameObject.SetActive(false);
                GameplayExpanded.gameObject.SetActive(false);
            }
        }
    }
    public void NewGame()
    {
        StartCoroutine(ExpandOnPlay());
    }

    public void Continue()
    {
        if (isFirstSave == 1)
        {
            StartCoroutine(LoadAsynchronously(PlayerPrefs.GetString("lastActiveScene")));
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            StartCoroutine(ChangeContinueText());
        }
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
        else if (!clickedOnSettings && !clickedOnVideo && !clickedOnAudio && !clickedOnGameplay)
        {
            menuExpand.Play("OptionExpandedNewSlideIn");
            bookText.Play("BookMoveUp");
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnVideo)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnAudio)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnGameplay)
        {
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
        else if (!clickedOnChapters && !clickedOnVideo && !clickedOnAudio && !clickedOnGameplay)
        {
            menuExpand.Play("OptionExpandedNewSlideIn");
            settingText.Play("SettingsMoveUp");
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnVideo)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnAudio)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
        else if (clickedOnGameplay)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
    }

    public void Video()
    {
        clickedOnSettings = false;
        clickedOnVideo = true;
        VideoExpanded.gameObject.SetActive(true);
        SettingsExpanded.Play("SettingsOptionsTextFadeOut");
        SettingsExpanded.gameObject.SetActive(false);
        settingText.gameObject.GetComponent<TMP_Text>().text = "Video";
        VideoExpanded.Play("VideoExpandedFadeIn");
    }
    public void AudioSettings()
    {
        clickedOnSettings = false;
        clickedOnAudio = true;
        AudioExpanded.gameObject.SetActive(true);
        SettingsExpanded.Play("SettingsOptionsTextFadeOut");
        SettingsExpanded.gameObject.SetActive(false);
        settingText.gameObject.GetComponent<TMP_Text>().text = "Audio";
        AudioExpanded.Play("AudioExpandedFadeIn");
    }
    public void AudioSlider()
    {
        BGMusic.volume = audioSlider.GetComponent<Slider>().value;
    }

    public void InvertToggle()
    {
        invertAxis = InvertAxisTog.isOn;
        PlayerPrefs.SetInt("InvertAxis", boolToInt(invertAxis));
    }
    public void GameplaySettings()
    {
        clickedOnSettings = false;
        clickedOnGameplay = true;
        GameplayExpanded.gameObject.SetActive(true);
        SettingsExpanded.Play("SettingsOptionsTextFadeOut");
        SettingsExpanded.gameObject.SetActive(false);
        settingText.gameObject.GetComponent<TMP_Text>().text = "Gameplay";
        GameplayExpanded.Play("GameplayExpandedFadeIn");
    }


    public void LeftRes()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateResLabel();
    }

    public void RightRes()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resLabelText.text = resolutions[selectedRes].horizontal.ToString() + "x" + resolutions[selectedRes].vertical.ToString();
    }

    public void Apply()
    {
        if (VSyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, FSTog);
    }

    public void Back()
    {
        if (clickedOnSettings)
        {
            clickedOnSettings = false;
            SettingsExpanded.gameObject.SetActive(false);
            settingText.Play("SettingsMoveDown");
            SettingsExpanded.Play("SettingsOptionsTextFadeOut");
            menuExpand.Play("OptionExpandedNewSlideOut");
        }
        else if (clickedOnChapters)
        {
            clickedOnChapters = false;
            bookText.Play("BookMoveDown");
            ChaptersExpanded.gameObject.SetActive(false);
            ChaptersExpanded.Play("ChaptersOptionsTextFadeOut");
            menuExpand.Play("OptionExpandedNewSlideOut");
        }
        else if ((clickedOnVideo || clickedOnAudio || clickedOnGameplay) && !clickedOnChapters && !clickedOnSettings)
        {
            StartCoroutine(DelayExpandedMenuText());
        }
    }

    private IEnumerator DelayExpandedMenuText()
    {
        if (clickedOnSettings || ((clickedOnVideo || clickedOnAudio || clickedOnGameplay) && !clickedOnChapters && !clickedOnSettings))
        {
            SettingsExpanded.gameObject.SetActive(true);
            SettingsExpanded.Play("SettingsOptionsTextFadeIn");
            if (clickedOnVideo)
            {
                clickedOnSettings = true;
                clickedOnVideo = false;
                VideoExpanded.Play("VideoExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                VideoExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
            else if (clickedOnAudio)
            {
                clickedOnSettings = true;
                clickedOnAudio = false;
                AudioExpanded.Play("AudioExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                AudioExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
            else if (clickedOnGameplay)
            {
                clickedOnSettings = true;
                clickedOnGameplay = false;
                GameplayExpanded.Play("GameplayExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                GameplayExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
        }
        else if (clickedOnChapters || ((clickedOnVideo || clickedOnAudio || clickedOnGameplay) && !clickedOnChapters && !clickedOnSettings))
        {
            ChaptersExpanded.gameObject.SetActive(true);
            ChaptersExpanded.Play("ChaptersOptionsTextFadeIn");
            if (clickedOnVideo)
            {
                clickedOnChapters = true;
                clickedOnVideo = false;
                bookText.Play("BookMoveUp");
                settingText.Play("SettingsMoveDown");
                VideoExpanded.Play("VideoExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                VideoExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
            else if (clickedOnAudio)
            {
                clickedOnChapters = true;
                clickedOnAudio = false;
                bookText.Play("BookMoveUp");
                settingText.Play("SettingsMoveDown");
                AudioExpanded.Play("AudioExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                AudioExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
            else if (clickedOnGameplay)
            {
                clickedOnChapters = true;
                clickedOnGameplay = false;
                bookText.Play("BookMoveUp");
                settingText.Play("SettingsMoveDown");
                GameplayExpanded.Play("GameplayExpandedFadeOut");
                yield return new WaitForSeconds(0.4f);
                GameplayExpanded.gameObject.SetActive(false);
                settingText.gameObject.GetComponent<TMP_Text>().text = "Settings";
            }
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
        yield return new WaitForSeconds(1f);
        onTitleScreen = true;
    }

    private IEnumerator ExpandOnPlay()
    {
        menu.GetComponent<Animator>().Play("MainMenuExpandOnPlay");
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(LoadAsynchronously("Dream"));
        HandleProgress.currentChapter = 1;
        HandleProgress.currentChapterName = "Chapter One";
        HandleProgress.currentScene = "Chapter_one_first_dream";
        HandleProgress.firstPlaythrough = true;
        PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_home");
    }

    private IEnumerator titleScreenDisableDelay()
    {
        yield return new WaitForSeconds(1f);
        titleScreen.SetActive(false);
        menu.GetComponent<Animator>().Play("MainMenuFadeIn");
    }

    private IEnumerator ChangeContinueText()
    {
        continueButtonText.GetComponent<TextMeshProUGUI>().text = "How can you continue when you haven't even played yet?";
        yield return new WaitForSeconds(3f);
        continueButtonText.GetComponent<TextMeshProUGUI>().text = "Continue";
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
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
}
[System.Serializable]
public class Resolution
{
    public int horizontal, vertical;
}