using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class HandleTimeline : MonoBehaviour
{
    private static PlayableDirector timeline;
    public static bool timelineIsPlaying = false;
    public GameObject phone;
    public GameObject objectiveContainer;
    public GameObject objectiveText;
    private void Update()
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_first_dream")
        {
            timeline = GameObject.Find("Timeline Dream").GetComponent<PlayableDirector>();
            timeline.Play();
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_waking_up_from_first_dream")
        {
            timeline = GameObject.Find("Waking up from first Dream TImeline").GetComponent<PlayableDirector>();
            timeline.Play();
            HandleProgress.currentScene = "Chapter_one_getting_up_from_bed";
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_talking_to_mom" && HandleProgress.pickedUpKnife && HandleProgress.currentObjectiveIndex == 4)
        {
            Debug.Log("i should have been played by you");
            timeline = GameObject.Find("Kill Yourself TIimeline").GetComponent<PlayableDirector>();
            timeline.Play();
            HandleProgress.currentScene = "Chapter_one_kill_yourself";
        }
        Debug.Log("CurrentScene" + HandleProgress.currentScene);
        Debug.Log("knife is " + HandleProgress.pickedUpKnife);
        Debug.Log("Chapter number is " + HandleProgress.currentChapter);
        Debug.Log("This number should be 4 " + HandleProgress.currentObjectiveIndex);
        // timeline = GameObject.Find("Waking up from first Dream TImeline").GetComponent<PlayableDirector>();

        if (timeline.state == PlayState.Playing)
        {
            Debug.Log("Timeline is playing");
            timelineIsPlaying = true;
            phone.layer = LayerMask.NameToLayer("Default");
            objectiveContainer.SetActive(false);
            objectiveText.SetActive(false);
        }
        else
        {
            timelineIsPlaying = false;
            Debug.Log("Timeline is not playing");
            phone.layer = LayerMask.NameToLayer("Interactable");
            if (!DialogueManager.dialogueIsPlaying)
            {
                objectiveContainer.SetActive(true);
                objectiveText.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }

}
