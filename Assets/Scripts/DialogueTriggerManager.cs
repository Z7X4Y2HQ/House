using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueTriggerManager : MonoBehaviour
{
    public TextAsset Chapter_one_getting_up_from_bed;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (HandleProgress.currentScene == "Chapter_one_getting_up_from_bed" && HandleProgress.currentObjectiveIndex == 3)
            {
                StartCoroutine(waitBeforeStartingDialogue());
                HandleProgress.currentScene = "Chapter_one_thinking_about_restarting_because_of_sachi";
            }
        }
    }

    private IEnumerator waitBeforeStartingDialogue()
    {
        yield return new WaitForSeconds(3.1f);
        dialogueManager.EnterDialogueMode(Chapter_one_getting_up_from_bed);
    }
}
