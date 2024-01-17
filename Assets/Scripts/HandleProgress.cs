using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
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
        new Objective { description = "Press W, A, S, D to move around", isCompleted = false }, // case: 0
        new Objective { description = "Pick up your phone from the table", isCompleted = false }, // case: 1
        new Objective { description = "Press TAB to take out phone", isCompleted = false }, // case: 2
        new Objective { description = "", isCompleted = false }, // case: 3
        new Objective { description = "Find a Knife", isCompleted = false }, // case: 4
        new Objective { description = "Find out what place this is", isCompleted = false }, // case: 5
        new Objective { description = "Find the source of that voice", isCompleted = false }, // case: 6
        new Objective { description = "Pick up your phone and change for school", isCompleted = false }, // case: 7
        new Objective { description = "Go to School", isCompleted = false }, // case: 8
        new Objective { description = "Check out the crowd", isCompleted = false }, // case: 9
        new Objective { description = "Find your name in the class list", isCompleted = false }, // case: 10
        new Objective { description = "Ask around where the staff room is", isCompleted = false }, // case: 11
        new Objective { description = "Talk to a Teacher About your Name in Class list", isCompleted = false }, // case: 12
        new Objective { description = "Go straight to Class 2-A", isCompleted = false }, // case: 13
        new Objective { description = "Find a chair to sit down", isCompleted = false }, // case: 14
        new Objective { description = "Find a chair to sit down", isCompleted = false }, // case: 15
        new Objective { description = "Go back home", isCompleted = false }, // case: 16
        new Objective { description = "Go to your room", isCompleted = false }, // case: 17
        new Objective { description = "Change and go to bed", isCompleted = false }, // case: 18
        new Objective { description = "Confront Aoi", isCompleted = false }, // case: 19
    };

    public static bool firstPlaythrough;
    public static int currentChapter = 1;
    public static string currentChapterName;
    public static string currentScene;
    public static bool tutorialComplete = false;
    public static int currentObjectiveIndex;
    private bool pressW = false;
    private bool pressA = false;
    private bool pressS = false;
    private bool pressD = false;
    public static bool pickedUpPhone = false;
    public static bool pickedUpKnife = false;
    public static bool readyForSchool = false;
    public static bool walkedOutofSchool = false;
    public static bool inRoom = false;
    public static bool isSit14 = false;
    public static string locationText;
    public static string time;


    private float duration = 0.8f;

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
        // if (currentObjectiveIndex == 0)
        // {
        //     currentChapter = 1;
        //     currentScene = "Chapter_one_end_going_up_stairs_to_sleep";
        //     currentObjectiveIndex = 17;
        //     StartCoroutine(UpdateObjective());
        //     tutorialComplete = true;
        //     pickedUpPhone = true;
        // }
        // SceneManagerScript.currentCharacter = "Takahashi_Summer_school";

        objectiveContainerAnimator.Play("SlideInFromRightContainer");
        objectiveTextAnimator.Play("SlideInFromRightText");
        if (tutorialComplete)
        {
            location.text = PlayerPrefs.GetString("currentLocation");
            dateTime.text = PlayerPrefs.GetString("currentDateTime");
            locationContainerAnimator.gameObject.SetActive(true);
            locationContainerAnimator.Play("SlideInFromRightContainer");
            locationTextAnimator.gameObject.SetActive(true);
            locationTextAnimator.Play("SlideInFromRightText");
            dateTimeContainerAnimator.gameObject.SetActive(true);
            dateTimeContainerAnimator.Play("SlideInFromLeftContainer");
            dateTimeTextAnimator.gameObject.SetActive(true);
            dateTimeTextAnimator.Play("SlideInFromLeftText");
        }
        StartCoroutine(UpdateObjective());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "House 1f" || SceneManager.GetActiveScene().name == "House 2f")
        {
            location.text = "Home";
        }
        else if (SceneManager.GetActiveScene().name == "Town")
        {
            location.text = "Town";
        }
        else if (SceneManager.GetActiveScene().name == "School")
        {
            location.text = "Town";
        }
        else if (SceneManager.GetActiveScene().name == "Hallway")
        {
            location.text = "School";
        }
        if (SceneManager.GetActiveScene().name != "Dream")
        {
            dateTime.text = time;
        }

        Debug.Log(SceneManagerScript.currentCharacter);
        if (!PauseMenuScript.gameIsPaused || currentObjectiveIndex != 0)
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
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 3: //
                bool objective3Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective3Complete")).value;
                locationText = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("location")).value;
                time = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("time")).value;
                if (objective3Complete)
                {
                    location.text = locationText;
                    dateTime.text = time;
                    locationContainerAnimator.gameObject.SetActive(true);
                    locationTextAnimator.gameObject.SetActive(true);
                    locationContainerAnimator.Play("SlideInFromRightContainer");
                    locationTextAnimator.Play("SlideInFromRightText");
                    dateTimeContainerAnimator.gameObject.SetActive(true);
                    dateTimeTextAnimator.gameObject.SetActive(true);
                    dateTimeContainerAnimator.Play("SlideInFromLeftContainer");
                    dateTimeTextAnimator.Play("SlideInFromLeftText");
                    tutorialComplete = true;
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 4: // Find a Knife
                Debug.Log("Case 4");
                if (HandleTimeline.killedYourself)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 5: // Find out what place this is
                Debug.Log("Case 5");
                location.text = "?????";
                dateTime.text = "??-??-????";
                bool objective5Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective5Complete")).value;
                locationText = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("location")).value;
                Debug.Log("Objective 5 Complete" + objective5Complete);
                if (objective5Complete)
                {
                    location.text = locationText;
                    objectives[currentObjectiveIndex].isCompleted = true;
                    Transform sachi = GameObject.Find("Sachi - NOT FINAL").transform;
                    sachi.position = new Vector3(0, 0, -18.06f);
                }
                break;
            case 6:
                Debug.Log("Case 6");
                if (HandleTimeline.objective6Complete)
                {
                    pickedUpKnife = false;
                    pickedUpPhone = false;
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 7:
                Debug.Log("Case 7");
                Debug.Log(currentObjectiveIndex);
                time = "2nd April, 2018";
                Debug.Log(SceneManagerScript.currentCharacter);
                if (pickedUpPhone && SceneManagerScript.currentCharacter == "Takahashi_Summer_school")
                {
                    readyForSchool = true;
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 8:
                Debug.Log("Case 8");
                if (DialogueTriggerManager.inSchool)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 9:
                Debug.Log("Case 9");
                bool objective9Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective9Complete")).value;
                if (objective9Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 10:
                Debug.Log("Case 10");
                bool objective10Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective10Complete")).value;
                if (objective10Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 11:
                Debug.Log("Case 11");
                bool objective11Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective11Complete")).value;
                if (objective11Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 12:
                Debug.Log("Case 12");
                bool objective12Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective12Complete")).value;
                bool goToClass = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("goToClass")).value;
                if (goToClass) //Timeline play krni
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                else
                {
                    if (objective12Complete) // Talk to a Teacher
                    {
                        objectives[currentObjectiveIndex].isCompleted = true;
                    }
                }
                break;
            case 13:
                Debug.Log("Case 13");
                locationText = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("location")).value;
                time = ((Ink.Runtime.StringValue)dialogueManager.GetVariableState("time")).value;
                location.text = locationText;
                dateTime.text = time;
                bool objective13Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective13Complete")).value;
                if (objective13Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 14:
                Debug.Log("Case 14");
                bool objective14Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective14Complete")).value;
                if (objective14Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 15:
                Debug.Log("Case 15");
                if (walkedOutofSchool)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 16:
                Debug.Log("Case 16");
                if (SceneManager.GetActiveScene().name == "House 1f")
                {
                    objective.text = "Let your mom know you're home";
                }
                bool objective16Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective16Complete")).value;
                if (objective16Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 17:
                Debug.Log("Case 17");
                if (inRoom)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 18:
                bool objective18Complete = ((Ink.Runtime.BoolValue)dialogueManager.GetVariableState("objective18Complete")).value;
                if (objective18Complete)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 19:

                break;
        }

        if (objectives[currentObjectiveIndex].isCompleted)
        {
            currentObjectiveIndex++;
            StartCoroutine(UpdateObjective());
        }

        Debug.Log("currentScene " + HandleProgress.currentScene);
        Debug.Log("currentObjectiveIndex " + HandleProgress.currentObjectiveIndex);
    }

    private IEnumerator UpdateObjective()
    {
        objectiveTextAnimator.Play("SlideOutFromRightText");
        yield return new WaitForSeconds(1.1f);
        objectiveTextAnimator.gameObject.SetActive(true);
        objective.text = objectives[currentObjectiveIndex].description;
        objectiveTextAnimator.Play("SlideInFromRightText");
    }
}
