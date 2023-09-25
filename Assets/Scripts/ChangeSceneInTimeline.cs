using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInTimeline : MonoBehaviour
{
     private void Update() {
        if (HandleTimeline.currentChapter == 1 && HandleTimeline.currentScene == "Chapter_one_first_dream"){
            HandleTimeline.currentScene = "Chapter_one_waking_up_from_first_dream";
            SceneManager.LoadScene("House 2f");
        }
    }
}
