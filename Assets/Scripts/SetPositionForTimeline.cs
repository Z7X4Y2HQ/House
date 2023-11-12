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
            characterController.enabled = false; // Disable the CharacterController temporarily.
            transform.position = new Vector3(115.4143f, 2.131f, -3.513328f);
            transform.rotation = Quaternion.Euler(0, -0.776f, 0);
            characterController.enabled = true;
        }
    }
}
