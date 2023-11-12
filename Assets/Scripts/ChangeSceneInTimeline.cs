using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInTimeline : MonoBehaviour
{
    private void Update()
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_first_dream")
        {
            HandleProgress.currentScene = "Chapter_one_waking_up_from_first_dream";
            SceneManager.LoadScene("House 2f");
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_kill_yourself")
        {
            HandleProgress.currentScene = "Chapter_one_dream_after_killing_yourself";
            SceneManager.LoadScene("Dream");
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_waking_up_from_the_second_dream")
        {
            HandleProgress.currentScene = "Chapter_one_going_to_school_after_the_second_dream";
            SceneManager.LoadScene("House 2f");
        }
    }
}
