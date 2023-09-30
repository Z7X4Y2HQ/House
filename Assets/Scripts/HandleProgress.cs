using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleProgress : MonoBehaviour
{

    public class Objective
    {
        public string description;
        public bool isCompleted;
    }

    public Objective[] objectives = new Objective[]
    {
        new Objective { description = "Press W, A, S, D to move around", isCompleted = false },
        new Objective { description = "Pick up your phone from the table", isCompleted = false },
    };

    public static bool firstPlaythrough;
    public static int currentChapter;
    public static string currentChapterName;
    public static string currentScene;
    public static bool tutorialComplete = false;
    private int currentObjectiveIndex = 0;
    private bool pressW = false;
    private bool pressA = false;
    private bool pressS = false;
    private bool pressD = false;

    public static TextMeshProUGUI objective;
    public static TextMeshProUGUI dateTime;
    public static TextMeshProUGUI location;
    public Animator ContainerAnimator;
    public Animator TextAnimator;

    private void Awake()
    {
        objective = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
        firstPlaythrough = true;
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
                if (pressW && pressA && pressS && pressD)
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
                break;
            case 1: // Objective: Press W, A, S, D to move around
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    objectives[currentObjectiveIndex].isCompleted = true;
                }
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
