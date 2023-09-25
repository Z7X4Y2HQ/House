using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject continueButton;
    private SwapCharacter SwapCharacter;
    public static int isFirstSave = 0;
    private string savedPlayer;
    private GameObject currentPlayer;
    public GameObject LoadingScreen;
    public Slider slider;

    private void Awake()
    {
        isFirstSave = PlayerPrefs.GetInt("isFirstSave");
        if (isFirstSave == 1)
        {
            continueButton.SetActive(true);
            newGameButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    public void NewGame()
    {
        StartCoroutine(LoadAsynchronously("Dream"));
        HandleTimeline.currentChapter = 1;
        HandleTimeline.currentScene = "Chapter_one_first_dream";
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
            SwapCharacter.Swap(SwapCharacter.Takahashi_Summer_school);
            currentPlayer = GameObject.FindWithTag("Takahashi_Summer_school");
        }

        Vector3 newPlayerPosition = new Vector3(PlayerPrefs.GetFloat("playerPositionX"), PlayerPrefs.GetFloat("playerPositionY"), PlayerPrefs.GetFloat("playerPositionZ"));
        Quaternion newPlayerRotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("playerRotationY"), 0);

        currentPlayer.transform.position = newPlayerPosition;
        currentPlayer.transform.eulerAngles = newPlayerRotation.eulerAngles;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
