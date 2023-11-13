using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionForTimeline : MonoBehaviour
{
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_talking_to_mom" && HandleProgress.pickedUpKnife && HandleProgress.currentObjectiveIndex == 4)
        {
            Transform TakahashisMom = GameObject.Find("Takahashi's Mom").transform;
            TakahashisMom.position = new Vector3(115.903999f, 2.16299963f, -1.47899997f);
            TakahashisMom.rotation = Quaternion.Euler(0, 0, 0);
            characterController.enabled = false;
            transform.position = new Vector3(115.4143f, 2.131f, -3.513328f);
            transform.rotation = Quaternion.Euler(0, -0.776f, 0);
            characterController.enabled = true;
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_waking_up_from_the_second_dream" && HandleProgress.currentObjectiveIndex == 6 && TimelineTriggerManager.timelineIsPlaying)
        {
            Debug.Log("Player position should have changed");
            characterController.enabled = false;
            transform.position = new Vector3(0, 0, -11f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterController.enabled = true;
        }

        Debug.Log("Chapter number " + HandleProgress.currentChapter);
        Debug.Log("Current Scene " + HandleProgress.currentScene);
        Debug.Log("Objective Index " + HandleProgress.currentObjectiveIndex);
        Debug.Log("Is Timeline Playing " + TimelineTriggerManager.timelineIsPlaying);
    }
}
