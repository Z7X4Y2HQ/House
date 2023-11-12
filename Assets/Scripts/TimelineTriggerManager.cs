using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineTriggerManager : MonoBehaviour
{
    public static PlayableDirector timeline;
    public static bool timelineIsPlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_follow_the_voice" && HandleProgress.currentObjectiveIndex == 6)
        {
            timeline = GameObject.Find("Talking to the female voice Timeline").GetComponent<PlayableDirector>();
            timeline.Play();
            timelineIsPlaying = true;
            HandleProgress.currentScene = "Chapter_one_waking_up_from_the_second_dream";
        }
    }
}
