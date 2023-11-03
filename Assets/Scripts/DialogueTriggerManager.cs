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
    private DialogueManager dialogueManager;
    public PlayableDirector momsWalkingFromKitchenAnimation;


    private void Awake()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
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
        }
        Debug.Log("currentScene " + HandleProgress.currentScene);
        Debug.Log("currentObjectiveIndex " + HandleProgress.currentObjectiveIndex);
        Debug.Log("gameObject Name" + gameObject.name);
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
