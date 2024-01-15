using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChangeSceneInTimeline : MonoBehaviour
{
    public Image LoadingScreen;
    public Slider slider;
    public Image sliderBG;
    private TMP_Text Tips;
    public GameObject DialogueChapter_one_making_up_a_stupid_lie;

    private void Start()
    {
        LoadingScreen.gameObject.SetActive(false);
        Tips = LoadingScreen.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        sliderBG = slider.transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_first_dream")
        {
            HandleProgress.currentScene = "Chapter_one_waking_up_from_first_dream";
            StartCoroutine(LoadAsynchronously("House 2f"));
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_kill_yourself")
        {
            HandleProgress.currentScene = "Chapter_one_dream_after_killing_yourself";
            // PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_school");
            // SceneManagerScript.currentCharacter = PlayerPrefs.GetString("currentCharacter");
            StartCoroutine(LoadAsynchronously("Dream"));
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_waking_up_from_the_second_dream")
        {
            HandleProgress.currentScene = "Chapter_one_going_to_school_after_the_second_dream";
            StartCoroutine(LoadAsynchronously("House 2f"));
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_going_into_staffroom")
        {
            HandleProgress.currentScene = "Chapter_one_standing_in_the_school_hallway";
            StartCoroutine(LoadAsynchronously("Hallway"));
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_making_up_a_stupid_lie")
        {
            DialogueChapter_one_making_up_a_stupid_lie.SetActive(true);
            GameObject.Find("Eiji").GetComponent<Animator>().SetBool("isStanding", true);
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_ending")
        {
            HandleProgress.walkedOutofSchool = true;
            HandleProgress.currentScene = "Chapter_one_end_walking_out_of_school";
            StartCoroutine(LoadAsynchronously("School"));
        }
    }
    IEnumerator LoadAsynchronously(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        LoadingScreen.gameObject.SetActive(true);
        if (SceneName == "Dream")
        {
            LoadingScreen.color = Color.white;
            sliderBG.color = Color.white;
            Tips.color = Color.white;
        }

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
