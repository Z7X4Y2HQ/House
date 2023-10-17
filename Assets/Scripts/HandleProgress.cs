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
        new Objective { description = "Kill Yourself!", isCompleted = false },
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
    public static bool pickedUpPhone = false;
    public static bool pickedUpKnife = false;

    [Header("Objective")]
    public static TextMeshProUGUI objective;
    public Animator objectiveContainerAnimator;
    public Animator objectiveTextAnimator;
    [Header("Location")]
    public static TextMeshProUGUI location;
    public Animator locationContainerAnimator;
    public Animator locationTextAnimator;

    [Header("Date Time")]
    public static TextMeshProUGUI dateTime;
    public Animator dateTimeContainerAnimator;
    public Animator dateTimeTextAnimator;


    [Header("Dialogue Files")]
    private DialogueManager dialogueManager;

    private void Awake()
    {
        objective = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
        location = GameObject.Find("Location Text").GetComponent<TextMeshProUGUI>();
        dateTime = GameObject.Find("Date TIme Text").GetComponent<TextMeshProUGUI>();
        phoneManager = GameObject.Find("PhoneManager").GetComponent<PhoneManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void Start()
    {
        objectiveContainerAnimator.Play("SlideInFromRightContainer");
        objectiveTextAnimator.Play("SlideInFromRightText");
        if (tutorialComplete)
        {
            location.text = PlayerPrefs.GetString("currentLocation");
            dateTime.text = PlayerPrefs.GetString("currentDateTime");
            locationContainerAnimator.gameObject.SetActive(true);
            locationTextAnimator.gameObject.SetActive(true);
            dateTimeContainerAnimator.gameObject.SetActive(true);
            dateTimeTextAnimator.gameObject.SetActive(true);
            locationContainerAnimator.Play("SlideInFromRightContainer");
            locationTextAnimator.Play("SlideInFromRightText");
            dateTimeContainerAnimator.Play("SlideInFromLeftContainer");
            dateTimeTextAnimator.Play("SlideInFromLeftText");
        }
        else
        {
            locationContainerAnimator.gameObject.SetActive(false);
            locationTextAnimator.gameObject.SetActive(false);
            dateTimeContainerAnimator.gameObject.SetActive(false);
            dateTimeTextAnimator.gameObject.SetActive(false);
        }
        StartCoroutine(UpdateObjective());
    }

    private void Update()
    {
        if (!PauseMenuScript.gameIsPaused)
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
            case 3: //
                bool objectiveComplete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objectiveComplete")).value;
                string locationText = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("location")).value;
                string time = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("time")).value;
                if (objectiveComplete)
                {
                    location.text = locationText;
                    dateTime.text = time;
                    PlayerPrefs.SetString("currentLocation", locationText);
                    PlayerPrefs.SetString("currentDateTime", time);
                    locationContainerAnimator.gameObject.SetActive(true);
                    locationTextAnimator.gameObject.SetActive(true);
                    dateTimeContainerAnimator.gameObject.SetActive(true);
                    dateTimeTextAnimator.gameObject.SetActive(true);
                    tutorialComplete = true;
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 4: // Find a Knife
                Debug.Log("Case 4");
                if (pickedUpKnife)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 5:
                location.text = "Kill Yourself!";
                dateTime.text = "Kill Yourself!";
                Debug.Log("Case 5");
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
        if (currentObjectiveIndex != 5)
        {
            objectiveTextAnimator.Play("SlideOutFromRightText");
            yield return new WaitForSeconds(1.1f);
            objective.text = objectives[currentObjectiveIndex].description;
            objectiveTextAnimator.Play("SlideInFromRightText");
        }
        else
        {
            objective.text = objectives[currentObjectiveIndex].description;
        }

    }
}
