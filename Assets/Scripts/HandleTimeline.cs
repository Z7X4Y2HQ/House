using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class HandleTimeline : MonoBehaviour
{
    public static PlayableDirector timeline;
    public static bool timelineIsPlaying = false;
    public GameObject phone;
    public GameObject objectiveContainer;
    public GameObject objectiveText;
    public Animator playIcon;
    public Animator pauseIcon;
    private bool timelinePaused = true;
    public static bool killedYourself = false;
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
            timeline = GameObject.Find("Kill Yourself TIimeline").GetComponent<PlayableDirector>();
            timeline.Play();
            HandleProgress.currentScene = "Chapter_one_kill_yourself";
        }
        else if (HandleProgress.currentChapter == 1 && HandleProgress.currentScene == "Chapter_one_dream_after_killing_yourself" && HandleProgress.currentObjectiveIndex == 4)
        {
            PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_school");
            SceneManagerScript.currentCharacter = PlayerPrefs.GetString("currentCharacter");
            timeline = GameObject.Find("Dream after killing youself Timeline").GetComponent<PlayableDirector>();
            timeline.Play();
            killedYourself = true;
            HandleProgress.currentScene = "Chapter_one_second_dream";
        }
        // timeline = GameObject.Find("Dream after killing youself Timeline").GetComponent<PlayableDirector>();

        if (timeline != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (timelinePaused)
                {
                    PauseTimeline();
                }
                else
                {
                    ResumeTimeline();
                }
            }
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
                // this.gameObject.SetActive(false);
            }
        }
    }

    void PauseTimeline()
    {
        Debug.Log("why it didn't work here, it should have");
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0.0f);
        timelinePaused = false;
        StartCoroutine(setPauseFalse());
    }

    void ResumeTimeline()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1.0f);
        timelinePaused = true;
        StartCoroutine(setPlayFalse());
    }

    IEnumerator setPauseFalse()
    {
        pauseIcon.SetBool("isPause", true);
        yield return new WaitForSeconds(0.4f);
        pauseIcon.SetBool("isPause", false);
    }
    IEnumerator setPlayFalse()
    {
        playIcon.SetBool("isPlay", true);
        yield return new WaitForSeconds(0.4f);
        playIcon.SetBool("isPlay", false);
    }

}
