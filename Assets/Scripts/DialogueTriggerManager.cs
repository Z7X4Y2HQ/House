using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Playables;


public class DialogueTriggerManager : MonoBehaviour
{
    public TextAsset Chapter_one_getting_up_from_bed;
    public TextAsset Chapter_one_walking_down_the_stairs;
    public TextAsset Chapter_one_talking_to_mom;
    public TextAsset Chapter_one_dream_after_killing_yourself;
    public TextAsset Chapter_one_waking_up_from_second_dream;
    public TextAsset Chapter_one_in_the_school_entering_the_crowd;
    public TextAsset Chapter_one_finding_name_in_list;
    public TextAsset Chapter_one_talking_to_student_about_staffroom;
    private DialogueManager dialogueManager;
    public PlayableDirector momsWalkingFromKitchenAnimation;
    public static bool inSchool;


    private void Awake()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (HandleProgress.currentScene == "Chapter_one_first_time_in_school" && HandleProgress.currentObjectiveIndex == 8 && gameObject.name == "EnterSchoolTrigger")
        {
            inSchool = true;
            HandleProgress.currentScene = "Chapter_one_in_the_school_entering_the_crowd";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (HandleProgress.currentScene == "Chapter_one_getting_up_from_bed" && HandleProgress.currentObjectiveIndex == 3 && gameObject.name == "DialogueChapter_one_getting_up_from_bed")
            {
                StartCoroutine(waitBeforeStartingDialogue());
                HandleProgress.currentScene = "Chapter_one_thinking_about_restarting_because_of_sachi";
            }
            if (HandleProgress.currentScene == "Chapter_one_thinking_about_restarting_because_of_sachi" && HandleProgress.currentObjectiveIndex == 4 && gameObject.name == "DialogueChapter_one_walking_down_the_stairs")
            {
                dialogueManager.EnterDialogueMode(Chapter_one_walking_down_the_stairs);
                HandleProgress.currentScene = "Chapter_one_walking_down_the_stairs";
            }
            if (HandleProgress.currentScene == "Chapter_one_walking_down_the_stairs" && HandleProgress.currentObjectiveIndex == 4 && gameObject.name == "DialogueChapter_one_talking_to_mom")
            {
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                StartCoroutine(playAnimationBeforeDialogue());
                HandleProgress.currentScene = "Chapter_one_talking_to_mom";
            }
            if (HandleProgress.currentScene == "Chapter_one_second_dream" && HandleProgress.currentObjectiveIndex == 5 && gameObject.name == "DialogueChapter_one_dream_after_killing_youself")
            {
                PlayerPrefs.SetString("currentCharacter", "Takahashi_Summer_school");
                SceneManagerScript.currentCharacter = PlayerPrefs.GetString("currentCharacter");
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_dream_after_killing_yourself);
                HandleProgress.currentScene = "Chapter_one_follow_the_voice";
            }
            if (HandleProgress.currentScene == "Chapter_one_second_dream_after_effects" && HandleProgress.currentObjectiveIndex == 7 && gameObject.name == "DialogueChapter_one_waking_up_from_second_dream")
            {
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_waking_up_from_second_dream);
                HandleProgress.currentScene = "Chapter_one_first_time_in_school";
            }
            if (HandleProgress.currentScene == "Chapter_one_in_the_school_entering_the_crowd" && HandleProgress.currentObjectiveIndex == 9 && gameObject.name == "DialogueChapter_one_in_the_school_entering_the_crowd")
            {
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_in_the_school_entering_the_crowd);
                HandleProgress.currentScene = "Chapter_one_finding_name_in_list";
            }
            if (HandleProgress.currentScene == "Chapter_one_finding_name_in_list" && HandleProgress.currentObjectiveIndex == 10 && gameObject.name == "DialogueChapter_one_finding_name_in_list")
            {
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_finding_name_in_list);
                HandleProgress.currentScene = "Chapter_one_talking_to_student_about_staffroom";
            }
            if (HandleProgress.currentScene == "Chapter_one_talking_to_student_about_staffroom" && HandleProgress.currentObjectiveIndex == 11 && gameObject.name == "DialogueChapter_one_talking_to_student_about_staffroom")
            {
                GameObject.Find(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_talking_to_student_about_staffroom);
                HandleProgress.currentScene = "Chapter_one_going_into_school";
            }
        }
        Debug.Log("currentObjectiveIndex " + HandleProgress.currentObjectiveIndex);
    }

    private IEnumerator playAnimationBeforeDialogue()
    {
        DialogueManager.dialogueIsPlaying = true;
        momsWalkingFromKitchenAnimation.Play();
        Debug.Log("starts here xD should be 4.9");
        yield return new WaitForSeconds(4.9f);
        Debug.Log("should have finished by now");
        dialogueManager.EnterDialogueMode(Chapter_one_talking_to_mom);
    }
    private IEnumerator waitBeforeStartingDialogue()
    {
        yield return new WaitForSeconds(3.1f);
        dialogueManager.EnterDialogueMode(Chapter_one_getting_up_from_bed);
    }
}
