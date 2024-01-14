using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Playables;


public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset[] inkJSON;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    [Header("Timelines")]
    private PlayableDirector timeline;

    [Header("Hide UI")]
    private GameObject HUD;

    private Story currentStory;
    public static bool dialogueIsPlaying;
    private bool canContinueToNextLine = false;
    private bool canSkip = false;
    private bool submitSkip = false;
    public static DialogueManager instance;
    private LockMouse lockMouse;



    private const string SPEAKER_TAG = "speaker";
    private const string ANIMATION_BOOL_TAG = "animationBool";
    private const string ANIMATION_TAG = "animation";
    private const string TIMELINE_TAG = "timeline";
    private string currentSpeaker;
    private string currentPlayerAnimator;

    private Coroutine displayLineCoroutine;
    private DialogueVariables dialogueVariables;
    private PhoneManager phoneManager;

    private void Awake()
    {
        lockMouse = GameObject.Find("LockMouse").GetComponent<LockMouse>();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
        }
        instance = this;
        HUD = GameObject.Find("HUD");
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        phoneManager = GameObject.Find("PhoneManager").GetComponent<PhoneManager>();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        Debug.Log("is dialogue playing is " + dialogueIsPlaying);
        if (PlayerManager.currentCollidingNPCName != null && Input.GetKeyDown(KeyCode.F) && !dialogueIsPlaying)
        {
            Debug.Log(PlayerManager.currentCollidingNPCName);
            foreach (TextAsset ink in inkJSON)
            {
                if (ink.name == PlayerManager.currentCollidingNPCName)
                {
                    EnterDialogueMode(ink);
                }
            }

        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && dialogueIsPlaying)
        {
            submitSkip = true;
        }
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        lockMouse.Unlock();
        if (phoneManager.phoneOut)
        {
            phoneManager.putBackPhone();
        }
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        displayNameText.text = "???";
        HUD.SetActive(false);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        lockMouse.Lock();
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        hideChoices();
        dialogueText.text = "";
        HUD.SetActive(true);
    }

    private void hideChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Invalid tag format: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();


            switch (tagKey)
            {
                case SPEAKER_TAG:
                    currentSpeaker = tagValue;
                    displayNameText.text = tagValue;
                    break;
                case ANIMATION_BOOL_TAG:
                    if (currentSpeaker == "Takahashi Tanjiro" || currentSpeaker == "Takahashi thinking")
                    {
                        if (tagValue == "isWalk")
                        {
                            GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool(tagValue, false);
                        }
                        else
                        {
                            if (tagValue == "isPhoneIn")
                            {
                                GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool("isPhoneUse", false);
                            }
                            GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool(tagValue, true);
                            StartCoroutine(waitForAnimation(tagValue));
                        }
                    }
                    else if (currentSpeaker == "Sachi")
                    {
                        GameObject.Find("Sachi - NOT FINAL").GetComponent<Animator>().SetBool(tagValue, true);
                    }
                    else if (currentSpeaker == "Nerd Student")
                    {
                        GameObject.Find("Std11 (1)").GetComponent<Animator>().SetBool(tagValue, true);
                        StartCoroutine(waitForAnimation(tagValue));
                    }
                    else if (currentSpeaker == "Young Teacher")
                    {
                        GameObject.Find("Teacher").GetComponent<Animator>().SetBool(tagValue, true);
                        StartCoroutine(waitForAnimation(tagValue));
                    }
                    break;
                case ANIMATION_TAG:
                    if (currentSpeaker == "Takahashi Tanjiro")
                    {
                        GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().Play(tagValue);
                    }
                    else if (currentSpeaker == "Sachi")
                    {
                        GameObject.Find("Sachi - NOT FINAL").GetComponent<Animator>().Play(tagValue);
                    }
                    break;
                case TIMELINE_TAG:
                    if (currentSpeaker == "Takahashi's Mom")
                    {
                        Debug.Log("This true");
                        if (tagValue == "Mom's Walk from near Sofa to Kitchen")
                        {
                            Debug.Log("This true too");
                            timeline = GameObject.Find("Mom's Walk from near Sofa to Kitchen").GetComponent<PlayableDirector>();
                            timeline.Play();
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("The tag came in and isn't being handled currently" + tag);
                    break;
            }
        }
    }
    private IEnumerator CanSkip()
    {
        canSkip = false; //Making sure the variable is false.
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

    private IEnumerator waitForAnimation(string tagValue)
    {
        if (tagValue == "isPhoneUse")
        {
            yield return new WaitForSeconds(999f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (currentSpeaker == "Takahashi Tanjiro" || currentSpeaker == "Takahashi thinking")
        {
            GameObject.FindWithTag(SceneManagerScript.currentCharacter).GetComponent<Animator>().SetBool(tagValue, false);
        }
        else if (currentSpeaker == "Nerd Student")
        {
            GameObject.Find("Std11 (1)").GetComponent<Animator>().SetBool(tagValue, false);
        }
        else if (currentSpeaker == "Young Teacher")
        {
            GameObject.Find("Teacher").GetComponent<Animator>().SetBool(tagValue, false);
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        hideChoices();
        submitSkip = false;
        canContinueToNextLine = false;

        StartCoroutine(CanSkip());

        foreach (char letter in line.ToCharArray())
        {
            if (canSkip && submitSkip)
            {
                submitSkip = false;
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(typingSpeed);
        }
        DisplayChoices();

        canContinueToNextLine = true;
        canSkip = false;
    }

    private void DisplayChoices()
    {
        EventSystem.current.SetSelectedGameObject(null);

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("There are more choices than there are choice buttons!");
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableName == null)
        {
            Debug.LogWarning("Variable " + variableName + " was found to be null");
        }
        return variableValue;
    }

    // public void OnApplicationQuit()
    // {
    //     if (dialogueVariables != null)
    //     {
    //         dialogueVariables.SaveVariables();
    //     }
    // }
}
