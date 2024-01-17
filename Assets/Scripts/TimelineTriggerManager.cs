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
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentObjectiveIndex == 17 && HandleProgress.currentScene == "Chapter_one_end_going_up_stairs_to_sleep" && gameObject.name == "Going to bed Chapter one End Scene")
        {
            HandleProgress.inRoom = true;
            HandleProgress.currentScene = "Chapter_one_end_going_to_bed";
        }
        else if (HandleProgress.currentChapter == 1 && SceneManagerScript.currentCharacter == "Takahashi_Summer_home" && HandleProgress.currentScene == "Chapter_one_end_going_to_bed" && HandleProgress.currentObjectiveIndex == 18 && gameObject.name == "Bed")
        {
            StartCoroutine(setPositionForTimeline());
            HandleProgress.objective.text = "";
        }

    }
    IEnumerator setPositionForTimeline()
    {
        CharacterController characterController = FindObjectOfType<CharacterController>();
        characterController.enabled = false;
        HandleProgress.pickedUpPhone = false;

        characterController.gameObject.transform.position = new Vector3(72.137001f, 5.39400005f, -15.9530001f);
        characterController.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        characterController.enabled = true;
        yield return new WaitForSeconds(0.2f);
        HandleTimeline.timeline = GameObject.Find("Going to bed Timeline").GetComponent<PlayableDirector>();
        HandleTimeline.timeline.Play();
        HandleTimeline.timelineIsPlaying = true;
        HandleProgress.currentScene = "Chapter_two_waking_up_after_a_week";
    }
}
