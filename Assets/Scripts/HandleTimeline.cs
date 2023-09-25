using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class HandleTimeline : MonoBehaviour
{
    public static int currentChapter;
    public static string currentScene;    
    private PlayableDirector timeline;

    private void Update() {
        if(currentChapter == 1 && currentScene == "Chapter_one_first_dream"){
            timeline = GameObject.Find("Timeline Dream").GetComponent<PlayableDirector>();
            timeline.Play();
        }
        else if (currentChapter == 1 && currentScene == "Chapter_one_waking_up_from_first_dream"){
            timeline = GameObject.Find("Waking up from first Dream TImeline").GetComponent<PlayableDirector>();
            timeline.Play();
            gameObject.SetActive(false);
        }
    }

}
