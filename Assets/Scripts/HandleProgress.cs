using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleProgress : MonoBehaviour
{
    private PhoneManager phoneManager;

    public class Objective
    {
        public string description;
        public bool isCompleted;
    }

    public Objective[] objectives = new Objective[]
    {
        new Objective { description = "Press W, A, S, D to move around", isCompleted = false},
        new Objective { description = "Pick up your phone from the table", isCompleted = false},
        new Objective { description = "Press TAB to take out phone", isCompleted = false },
        new Objective { description = "", isCompleted = false },
        new Objective { description = "Find a Knife", isCompleted = false },
    };

    public static bool firstPlaythrough;
    public static int currentChapter;
    public static string currentChapterName;
    public static string currentScene;
    public static bool tutorialComplete = false;
    public static int currentObjectiveIndex = 0;
    private bool pressW = false;
    private bool pressA = false;
    private bool pressS = false;
    private bool pressD = false;
    public bool pickedUpPhone = false;


    public static TextMeshProUGUI objective;
    public static TextMeshProUGUI dateTime;
    public static TextMeshProUGUI location;
    public Animator ContainerAnimator;
    public Animator TextAnimator;

    [Header("Dialogue Files")]
    private DialogueManager dialogueManager;

    private void Awake()
    {
        objective = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
        phoneManager = GameObject.Find("PhoneManager").GetComponent<PhoneManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void Start()
    {
        ContainerAnimator.Play("SlideInFromRightContainer");
        TextAnimator.Play("SlideInFromRightText");
        StartCoroutine(UpdateObjective());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            pressW = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressA = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pressS = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pressD = true;
        }
        switch (currentObjectiveIndex)
        {
            case 0: // Objective: Press W, A, S, D to move around
                firstPlaythrough = true;
                if (pressW && pressA && pressS && pressD)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 1: // Objective: Pick up your phone from the table
                if (pickedUpPhone)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 2: // Objective: Press TAB to take out phone
                if (phoneManager.phoneOutFirstTime)
                {
                    HandleProgress.currentScene = "Chapter_one_getting_up_from_bed"; //TEMPPPPP
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 3:
                bool objectiveComplete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objectiveComplete")).value;
                if (objectiveComplete)
                {
                    tutorialComplete = true;
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 4:
                Debug.Log("Case 4");
                break;
        }


        if (objectives[currentObjectiveIndex].isCompleted)
        {
            currentObjectiveIndex++;
            StartCoroutine(UpdateObjective());
        }
    }

    private IEnumerator UpdateObjective()
    {
        TextAnimator.Play("SlideOutFromRightText");
        yield return new WaitForSeconds(1.1f);
        objective.text = objectives[currentObjectiveIndex].description;
        TextAnimator.Play("SlideInFromRightText");
    }
}
