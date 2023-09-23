using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUD;
    private GameObject player;
    PlayerInput PlayerInput;

    void Awake()
    {
        PlayerInput = new PlayerInput();

        if (playerInWardrobeRange.currentCloths == null)
        {
            playerInWardrobeRange.currentCloths = PlayerPrefs.GetString("currentCharacter");
        }
    }
    void Update()
    {
        if (PlayerInput.UI.Pause.triggered)
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
        player = GameObject.FindWithTag(playerInWardrobeRange.currentCloths);
        PlayerPrefs.SetInt("isFirstSave", 1);
        PlayerPrefs.SetString("lastActiveScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("currentCharacter", player.tag);
        PlayerPrefs.SetFloat("playerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("playerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("playerPositionZ", player.transform.position.z);
        PlayerPrefs.SetFloat("playerRotationY", player.transform.eulerAngles.y);
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

    void OnEnable()
    {
        PlayerInput.UI.Enable();
    }
    void OnDisable()
    {
        PlayerInput.UI.Disable();
    }

}
