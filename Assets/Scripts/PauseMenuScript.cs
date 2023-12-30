using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUD;
    private GameObject player;
    PlayerInput PlayerInput;
    private PhoneManager phoneManager;
    private LockMouse lockMouse;
    public GameObject settingMenu;
    public GameObject pauseMenu;
    public GameObject VideoMenu;
    public GameObject AudioMenu;
    public GameObject GameplayMenu;
    private bool isSettingOpen;
    private bool isVideoOpen;
    private bool isAudioOpen;
    private bool isGameplayOpen;
    public Toggle InvertTog, FSTog, VSyncTog;
    private CinemachineFreeLook primaryCamera;
    private bool foundRes = false;
    private SwapCharacter swapCharacter;


    public TMP_Text resLabelText;
    private int selectedRes;
    public List<Resolution> resolutions = new List<Resolution>();

    public TMP_Text MSAALabelText;
    private int selectedMSAA;
    public List<AntiAliasing> MSAAs = new List<AntiAliasing>();
    public TMP_Text ShadowLabelText;
    private int selectedShadow;
    public List<Shadow> ShadowList = new List<Shadow>();
    private UniversalRenderPipelineAsset urpAsset;


    void Awake()
    {
        urpAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
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
        if (urpAsset.msaaSampleCount == 1)
        {
            MSAALabelText.text = "Disabled";
        }
        else if (urpAsset.msaaSampleCount == 2)
        {
            MSAALabelText.text = "2x";
        }
        else if (urpAsset.msaaSampleCount == 4)
        {
            MSAALabelText.text = "4x";
        }
        else if (urpAsset.msaaSampleCount == 8)
        {
            MSAALabelText.text = "8x";
        }
        primaryCamera = GameObject.Find("Third person Camera").GetComponent<CinemachineFreeLook>();
        InvertTog.isOn = intToBool(PlayerPrefs.GetInt("InvertAxis"));
        lockMouse = GameObject.Find("LockMouse").GetComponent<LockMouse>();
        PlayerInput = new PlayerInput();

        if (playerInWardrobeRange.currentCloths == null)
        {
            playerInWardrobeRange.currentCloths = PlayerPrefs.GetString("currentCharacter");
        }
        phoneManager = GameObject.Find("PhoneManager").GetComponent<PhoneManager>();
    }
    void Update()
    {
        if (PlayerInput.UI.Pause.triggered && !DialogueManager.dialogueIsPlaying && !HandleTimeline.timelineIsPlaying)
        {
            if (phoneManager.phoneOut)
            {
                phoneManager.putBackPhone();
            }
            else if (SwapCharacter.isWardrobeOpen)
            {
                swapCharacter = FindObjectOfType<CharacterController>().GetComponent<SwapCharacter>();
                swapCharacter.CloseWardrobe();
            }
            else
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }
    }

    public void PhoneSettings()
    {
        Settings();
        HUD.SetActive(false);
        phoneManager.phoneUI.gameObject.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        lockMouse.Unlock();
        gameIsPaused = true;
    }

    public void Resume()
    {
        HUD.SetActive(true);
        phoneManager.phoneUI.gameObject.SetActive(true);
        pauseMenuUI.SetActive(false);
        isSettingOpen = false;
        isVideoOpen = false;
        isAudioOpen = false;
        isGameplayOpen = false;
        isSettingOpen = false;
        Time.timeScale = 1f;
        lockMouse.Lock();
        gameIsPaused = false;
    }
    public void Pause()
    {
        HUD.SetActive(false);
        phoneManager.phoneUI.gameObject.SetActive(false);
        pauseMenuUI.SetActive(true);
        pauseMenu.SetActive(true);
        VideoMenu.SetActive(false);
        settingMenu.SetActive(false);
        AudioMenu.SetActive(false);
        GameplayMenu.SetActive(false);
        Time.timeScale = 0f;
        lockMouse.Unlock();
        gameIsPaused = true;
    }
    public void Save()
    {
        player = GameObject.FindWithTag(SceneManagerScript.currentCharacter);
        PlayerPrefs.SetInt("isFirstSave", 1);
        PlayerPrefs.SetString("lastActiveScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("currentCharacter", player.tag);
        PlayerPrefs.SetFloat("playerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("playerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("playerPositionZ", player.transform.position.z);
        PlayerPrefs.SetFloat("playerRotationY", player.transform.eulerAngles.y);
        PlayerPrefs.SetInt("currentChapter", HandleProgress.currentChapter);
        PlayerPrefs.SetString("currentScene", HandleProgress.currentScene);
        PlayerPrefs.SetInt("currentObjectiveIndex", HandleProgress.currentObjectiveIndex);
        PlayerPrefs.SetInt("tutorialComplete", boolToInt(HandleProgress.tutorialComplete));
        PlayerPrefs.SetInt("pickedUpPhone", boolToInt(HandleProgress.pickedUpPhone));
        PlayerPrefs.SetInt("pickedUpKnife", boolToInt(HandleProgress.pickedUpKnife));
        PlayerPrefs.SetInt("readyForSchool", boolToInt(HandleProgress.readyForSchool));

    }

    public void WardrobeHome()
    {
        if (playerInWardrobeRange.currentCloths == "Takahashi_Summer_home")
        {
            StartCoroutine(ChangeWardrobeBottomText());
        }
        else
        {
            swapCharacter = FindObjectOfType<CharacterController>().GetComponent<SwapCharacter>();
            StartCoroutine(swapCharacter.SwapCloths(swapCharacter.Takahashi_Summer_home));
            SwapCharacter.isWardrobeOpen = false;
        }
    }
    public void WardrobeSchool()
    {
        if (playerInWardrobeRange.currentCloths == "Takahashi_Summer_school")
        {
            StartCoroutine(ChangeWardrobeBottomText());
        }
        else
        {
            swapCharacter = FindObjectOfType<CharacterController>().GetComponent<SwapCharacter>();
            StartCoroutine(swapCharacter.SwapCloths(swapCharacter.Takahashi_Summer_school));
            SwapCharacter.isWardrobeOpen = false;
        }

    }

    private IEnumerator ChangeWardrobeBottomText()
    {
        SwapCharacter.WardrobeSameClothText.gameObject.SetActive(true);
        SwapCharacter.WardrobeCloseText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        SwapCharacter.WardrobeSameClothText.gameObject.SetActive(false);
        SwapCharacter.WardrobeCloseText.gameObject.SetActive(true);
    }

    public void Settings()
    {
        isSettingOpen = true;
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Video()
    {
        isVideoOpen = true;
        isSettingOpen = false;
        settingMenu.SetActive(false);
        VideoMenu.SetActive(true);
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

    public void LeftMSAA()
    {
        selectedMSAA--;
        if (selectedMSAA < 0)
        {
            selectedMSAA = 0;
        }
        UpdateMSAALabel();
    }
    public void RightMSAA()
    {
        selectedMSAA++;
        if (selectedMSAA > MSAAs.Count - 1)
        {
            selectedMSAA = MSAAs.Count - 1;
        }
        UpdateMSAALabel();
    }

    public void LeftShadow()
    {
        selectedShadow--;
        if (selectedShadow < 0)
        {
            selectedShadow = 0;
        }
        UpdateShadowLabel();
    }
    public void RightShadow()
    {
        selectedShadow++;
        if (selectedShadow > ShadowList.Count - 1)
        {
            selectedShadow = ShadowList.Count - 1;
        }
        UpdateShadowLabel();
    }

    public void UpdateResLabel()
    {
        resLabelText.text = resolutions[selectedRes].horizontal.ToString() + "x" + resolutions[selectedRes].vertical.ToString();
    }
    public void UpdateMSAALabel()
    {
        MSAALabelText.text = MSAAs[selectedMSAA].value;
    }
    public void UpdateShadowLabel()
    {
        ShadowLabelText.text = ShadowList[selectedShadow].value;
    }
    public void Audio()
    {
        isAudioOpen = true;
        isSettingOpen = false;
        settingMenu.SetActive(false);
        AudioMenu.SetActive(true);
    }
    public void Gameplay()
    {
        isGameplayOpen = true;
        isSettingOpen = false;
        settingMenu.SetActive(false);
        GameplayMenu.SetActive(true);
    }
    public void InvertToggle()
    {
        PlayerPrefs.SetInt("InvertAxis", boolToInt(InvertTog.isOn));
        primaryCamera.m_YAxis.m_InvertInput = intToBool(PlayerPrefs.GetInt("InvertAxis"));
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
        if (MSAALabelText.text == "Disabled")
        {
            urpAsset.msaaSampleCount = 1;
        }
        else if (MSAALabelText.text == "2x")
        {
            urpAsset.msaaSampleCount = 2;
        }
        else if (MSAALabelText.text == "4x")
        {
            urpAsset.msaaSampleCount = 4;
        }
        else if (MSAALabelText.text == "8x")
        {
            urpAsset.msaaSampleCount = 8;
        }

        if (ShadowLabelText.text == "Low")
        {
            urpAsset.shadowCascadeCount = 1;
        }
        else if (ShadowLabelText.text == "Medium")
        {
            urpAsset.shadowCascadeCount = 2;
        }
        else if (ShadowLabelText.text == "High")
        {
            urpAsset.shadowCascadeCount = 3;
        }
        else if (ShadowLabelText.text == "Very High")
        {
            urpAsset.shadowCascadeCount = 4;
        }

        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, FSTog.isOn);
    }


    public void Back()
    {
        if (isSettingOpen)
        {
            isSettingOpen = false;
            settingMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else if (isVideoOpen)
        {
            isVideoOpen = false;
            isSettingOpen = true;
            settingMenu.SetActive(true);
            VideoMenu.SetActive(false);
        }
        else if (isAudioOpen)
        {
            isAudioOpen = false;
            isSettingOpen = true;
            settingMenu.SetActive(true);
            AudioMenu.SetActive(false);
        }
        else if (isGameplayOpen)
        {
            isGameplayOpen = false;
            isSettingOpen = true;
            settingMenu.SetActive(true);
            GameplayMenu.SetActive(false);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitFr()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    void OnEnable()
    {
        PlayerInput.UI.Enable();
    }
    void OnDisable()
    {
        PlayerInput.UI.Disable();
    }

}