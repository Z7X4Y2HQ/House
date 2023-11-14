using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUD;
    private GameObject player;
    PlayerInput PlayerInput;
    private PhoneManager phoneManager;

    void Awake()
    {
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
            else
            {
                if (gameIsPaused)
                {
                    Resume();
                    HUD.SetActive(true);
                }
                else
                {
                    Pause();
                    HUD.SetActive(false);
                }
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        gameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
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

    void OnEnable()
    {
        PlayerInput.UI.Enable();
    }
    void OnDisable()
    {
        PlayerInput.UI.Disable();
    }

}
