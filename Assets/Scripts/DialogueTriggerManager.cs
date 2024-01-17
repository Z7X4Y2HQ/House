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
    public TextAsset Chapter_one_standing_in_the_hallway;
    public TextAsset Chapter_one_standing_in_staffroom_door;
    public TextAsset Chapter_one_talking_to_teacher_about_list;
    public TextAsset Chapter_one_walking_out_of_staffroom;
    public TextAsset Chapter_one_walking_into_classroom;
    public TextAsset Chapter_one_making_up_a_stupid_lie;
    public TextAsset Chapter_one_end_walking_out_of_school;
    public TextAsset Chapter_one_end_talking_to_your_mom;
    public TextAsset Chapter_two_waking_up_after_week;

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
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                StartCoroutine(playAnimationBeforeDialogue());
                HandleProgress.currentScene = "Chapter_one_talking_to_mom";
            }
            if (HandleProgress.currentScene == "Chapter_one_second_dream" && HandleProgress.currentObjectiveIndex == 5 && gameObject.name == "DialogueChapter_one_dream_after_killing_youself")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_dream_after_killing_yourself);
                HandleProgress.currentScene = "Chapter_one_follow_the_voice";
            }
            if (HandleProgress.currentScene == "Chapter_one_second_dream_after_effects" && HandleProgress.currentObjectiveIndex == 7 && gameObject.name == "DialogueChapter_one_waking_up_from_second_dream")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_waking_up_from_second_dream);
                HandleProgress.currentScene = "Chapter_one_first_time_in_school";
            }
            if (HandleProgress.currentScene == "Chapter_one_in_the_school_entering_the_crowd" && HandleProgress.currentObjectiveIndex == 9 && gameObject.name == "DialogueChapter_one_in_the_school_entering_the_crowd")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_in_the_school_entering_the_crowd);
                HandleProgress.currentScene = "Chapter_one_finding_name_in_list";
            }
            if (HandleProgress.currentScene == "Chapter_one_finding_name_in_list" && HandleProgress.currentObjectiveIndex == 10 && gameObject.name == "DialogueChapter_one_finding_name_in_list")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_finding_name_in_list);
                HandleProgress.currentScene = "Chapter_one_talking_to_student_about_staffroom";
            }
            if (HandleProgress.currentScene == "Chapter_one_talking_to_student_about_staffroom" && HandleProgress.currentObjectiveIndex == 11 && gameObject.name == "DialogueChapter_one_talking_to_student_about_staffroom")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_talking_to_student_about_staffroom);
                HandleProgress.currentScene = "Chapter_one_going_into_staffroom";
            }
            if (HandleProgress.currentScene == "Chapter_one_going_into_staffroom" && HandleProgress.currentObjectiveIndex == 12 && gameObject.name == "DialogueChapter_one_standing_in_staffroom_door")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_standing_in_staffroom_door);
                HandleProgress.currentScene = "Chapter_one_about_to_talk_to_teacher";
            }
            if (HandleProgress.currentScene == "Chapter_one_about_to_talk_to_teacher" && HandleProgress.currentObjectiveIndex == 12 && gameObject.name == "DialogueChapter_one_talking_to_teacher_about_list")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_talking_to_teacher_about_list);
                HandleProgress.objective.text = "Walk out of the Staffroom";
                HandleProgress.currentScene = "Chapter_one_walking_out_of_staffroom";
            }
            if (HandleProgress.currentScene == "Chapter_one_walking_out_of_staffroom" && HandleProgress.currentObjectiveIndex == 12 && gameObject.name == "DialogueChapter_one_walking_out_of_staffroom")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_walking_out_of_staffroom);
                HandleProgress.currentScene = "Chapter_one_going_into_class";
            }
            if (HandleProgress.currentScene == "Chapter_one_standing_in_the_school_hallway" && HandleProgress.currentObjectiveIndex == 13 && gameObject.name == "DialogueChapter_one_standing_in_the_hallway")
            {
                dialogueManager.EnterDialogueMode(Chapter_one_standing_in_the_hallway);
                HandleProgress.currentScene = "Chapter_one_going_into_class";
                HandleTimeline.timelineIsPlaying = false;
            }
            if (HandleProgress.currentScene == "Chapter_one_going_into_class" && HandleProgress.currentObjectiveIndex == 13 && gameObject.name == "DialogueChapter_one_walking_into_classroom")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_walking_into_classroom);
                HandleProgress.currentScene = "Chapter_one_finding_a_seat_to_sit";
            }
            if (HandleProgress.currentScene == "Chapter_one_making_up_a_stupid_lie" && HandleProgress.currentObjectiveIndex == 14 && gameObject.name == "DialogueChapter_one_making_up_a_stupid_lie")
            {
                GameObject.Find("Eiji").GetComponent<Animator>().SetBool("isStanding", true);
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_making_up_a_stupid_lie);
                HandleProgress.currentScene = "Chapter_one_final_scene";
            }
            if (HandleProgress.currentScene == "Chapter_one_end_walking_out_of_school" && HandleProgress.currentObjectiveIndex == 16 && gameObject.name == "DialogueChapter_one_end_walking_out_of_school")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_one_end_walking_out_of_school);
                HandleProgress.currentScene = "Chapter_one_end_in_home";
            }
            if (HandleProgress.currentScene == "Chapter_one_end_in_home" && HandleProgress.currentObjectiveIndex == 16 && gameObject.name == "DialogueChapter_one_end_talking_to_your_mom")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                StartCoroutine(playAnimationBeforeDialogue2());
                HandleProgress.currentScene = "Chapter_one_end_going_up_stairs_to_sleep";
            }
            if (HandleProgress.currentScene == "Chapter_two_waking_up_after_a_week" && HandleProgress.currentObjectiveIndex == 18 && gameObject.name == "DialogueChapter_two_waking_up_after_week")
            {
                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isWalk", false);
                dialogueManager.EnterDialogueMode(Chapter_two_waking_up_after_week);
                HandleProgress.currentScene = "Chapter_two_going_to_school_after_week";
            }

        }
        Debug.Log("currentObjectiveIndex " + HandleProgress.currentObjectiveIndex);
    }

    private IEnumerator playAnimationBeforeDialogue()
    {
        DialogueManager.dialogueIsPlaying = true;
        HandleTimeline.timelineIsPlaying = true;
        momsWalkingFromKitchenAnimation.Play();
        Debug.Log("starts here xD should be 4.9");
        yield return new WaitForSeconds(4.9f);
        Debug.Log("should have finished by now");
        dialogueManager.EnterDialogueMode(Chapter_one_talking_to_mom);
        HandleTimeline.timelineIsPlaying = false;
    }
    private IEnumerator playAnimationBeforeDialogue2()
    {
        DialogueManager.dialogueIsPlaying = true;
        HandleTimeline.timelineIsPlaying = true;
        momsWalkingFromKitchenAnimation.Play();
        Debug.Log("starts here xD should be 4.9");
        yield return new WaitForSeconds(4.9f);
        Debug.Log("should have finished by now");
        dialogueManager.EnterDialogueMode(Chapter_one_end_talking_to_your_mom);
        HandleTimeline.timelineIsPlaying = false;
    }
    private IEnumerator waitBeforeStartingDialogue()
    {
        yield return new WaitForSeconds(3.1f);
        dialogueManager.EnterDialogueMode(Chapter_one_getting_up_from_bed);
    }
}
