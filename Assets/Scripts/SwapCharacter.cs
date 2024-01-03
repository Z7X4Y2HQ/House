using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using TMPro;
using UnityEngine.SceneManagement;


public class SwapCharacter : MonoBehaviour
{
    public GameObject Takahashi_Summer_home;
    public GameObject Takahashi_Summer_school;
    private Animator animator;
    private GameObject wardrobeCamera;
    private GameObject wardrobeClothsUI;
    public static GameObject WardrobeCloseText;
    public static GameObject WardrobeSameClothText;
    private GameObject HUD;
    public static bool isWardrobeOpen;
    private LockMouse lockMouse;
    private PauseMenuScript pauseMenuScript;
    private DialogueManager dialogueManager;
    public TextAsset wardrobe;


    void Awake()
    {
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenuScript>();
        animator = GameObject.Find("LevelLoader").GetComponentInChildren<Animator>();
        HUD = GameObject.Find("HUD");
        lockMouse = GameObject.Find("LockMouse").GetComponent<LockMouse>();
        wardrobeClothsUI = GameObject.Find("WardrobeClothsUI");
        WardrobeCloseText = wardrobeClothsUI.transform.GetChild(1).gameObject;
        WardrobeSameClothText = wardrobeClothsUI.transform.GetChild(3).gameObject;
        if (SceneManager.GetActiveScene().name == "House 2f")
        {
            wardrobeCamera = GameObject.Find("WardrobeCamera");
            wardrobeCamera.SetActive(false);
        }
        wardrobeClothsUI.SetActive(false);
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (playerInWardrobeRange.changeClothes)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isWardrobeOpen && HandleProgress.currentObjectiveIndex > 6)
            {
                OpenWardrobe();
            }
            if (Input.GetKeyDown(KeyCode.F) && !isWardrobeOpen && !DialogueManager.dialogueIsPlaying)
            {
                dialogueManager.EnterDialogueMode(wardrobe);
            }
        }
    }

    private void OpenWardrobe()
    {
        HUD = GameObject.Find("HUD");
        HUD.SetActive(false);
        lockMouse.Unlock();
        wardrobeClothsUI.SetActive(true);
        isWardrobeOpen = true;
        wardrobeCamera.SetActive(true);
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(71.76f, 5.41f, -13.3f);
        transform.eulerAngles = new Vector3(0, 50.23f, 0);
    }
    public void CloseWardrobe()
    {
        HUD.SetActive(true);
        lockMouse.Lock();
        wardrobeClothsUI.SetActive(false);
        gameObject.GetComponent<CharacterController>().enabled = true;
        isWardrobeOpen = false;
        wardrobeCamera.SetActive(false);
    }



    public IEnumerator SwapCloths(GameObject character)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.4f);
        Swap(character);
        pauseMenuScript.Resume();
        animator.Play("Crossfade End");
    }

    public void Swap(GameObject character)
    {
        Vector3 position = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;

        Destroy(this.gameObject);
        Instantiate(character, position, rotation);
    }
}
