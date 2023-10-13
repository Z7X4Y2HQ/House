using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class HandleTimeline : MonoBehaviour
{
    private PlayableDirector timeline;
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
        timeline = GameObject.Find("Waking up from first Dream TImeline").GetComponent<PlayableDirector>();


        if (timeline.state == PlayState.Playing)
        {
            Debug.Log("Timeline is playing");
            phone.layer = LayerMask.NameToLayer("Default");
            objectiveContainer.SetActive(false);
            objectiveText.SetActive(false);
        }
        else
        {
            Debug.Log("Timeline is not playing");
            phone.layer = LayerMask.NameToLayer("Interactable");
            objectiveContainer.SetActive(true);
            objectiveText.SetActive(true);
            // this.gameObject.SetActive(false);
        }
    }

}
