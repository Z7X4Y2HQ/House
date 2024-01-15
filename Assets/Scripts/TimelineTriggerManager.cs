using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineTriggerManager : MonoBehaviour
{
    // public static PlayableDirector timeline;
    public static bool timelineIsPlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentObjectiveIndex == 6 && HandleProgress.currentScene == "Chapter_one_follow_the_voice")
        {
            // PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_home");
            // SceneManagerScript.currentCharacter = PlayerPrefs.GetString("currentCharacter");
            HandleTimeline.timeline = GameObject.Find("Talking to the female voice Timeline").GetComponent<PlayableDirector>();
            HandleTimeline.timeline.Play();
            timelineIsPlaying = true;
            HandleProgress.currentScene = "Chapter_one_waking_up_from_the_second_dream";
        }
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentObjectiveIndex == 17 && HandleProgress.currentScene == "Chapter_one_end_going_up_stairs_to_sleep")
        {
            HandleProgress.inRoom = true;
        }

    }
}
