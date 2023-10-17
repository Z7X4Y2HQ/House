using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneManagerScript : MonoBehaviour
{
    PlayerInput PlayerInput;
    private bool playerInRange = false;
    private Animator animator;
    public string sceneToLoad;
    private GameObject player;
    private Transform spawnPoint;
    private static string playerPosition;
    public static string currentCharacter;
    private string currentCharacterInHierarchy;
    private SwapCharacter SwapCharacter;
    private void Awake()
    {
        PlayerInput = new PlayerInput();
        if (currentCharacter == null)
        {
            currentCharacter = PlayerPrefs.GetString("currentCharacter");
        }
        animator = GameObject.Find("LevelLoader").GetComponentInChildren<Animator>();
        currentCharacterInHierarchy = FindObjectOfType<CharacterController>().gameObject.tag;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerPosition = gameObject.name;
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = true;
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if (PlayerInput.Interact.InteractSceneChange.triggered)
            {
                StartCoroutine(LoadLevel(sceneToLoad));
            }
        }
    }

    IEnumerator LoadLevel(string sceneToLoad)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentCharacter = PlayerPrefs.GetString("currentCharacter");
        currentCharacterInHierarchy = FindObjectOfType<CharacterController>().gameObject.tag;
        SwapCharacter = FindObjectOfType<CharacterController>().GetComponent<SwapCharacter>();
        if (scene.name == sceneToLoad)
        {
            if (playerPosition == "BalconyToRoom")
            {
                spawnPoint = GameObject.Find("RoomToBalcony").transform.GetChild(0).gameObject.transform;
            }
            else if (playerPosition == "StairsToF2")
            {
                spawnPoint = GameObject.Find("StairsToF1").transform.GetChild(0).gameObject.transform;
            }
            else if (playerPosition == "RoomToBalcony")
            {
                spawnPoint = GameObject.Find("BalconyToRoom").transform.GetChild(0).gameObject.transform;
            }
            else if (playerPosition == "StairsToF1")
            {
                spawnPoint = GameObject.Find("StairsToF2").transform.GetChild(0).gameObject.transform;
            }

            if (currentCharacter == currentCharacterInHierarchy)
            {
                Debug.Log(1);
                player = GameObject.Find(currentCharacter);
            }
            else
            {
                if (currentCharacter == "Takahashi_Summer_home")
                {
                    Debug.Log(2);
                    SwapCharacter.Swap(SwapCharacter.Takahashi_Summer_home);
                    player = GameObject.Find("Takahashi_Summer_home(Clone)");
                }
                else if (currentCharacter == "Takahashi_Summer_school")
                {
                    Debug.Log(3);
                    SwapCharacter.Swap(SwapCharacter.Takahashi_Summer_school);
                    player = GameObject.Find("Takahashi_Summer_school(Clone)");
                }
            }

            player.transform.position = spawnPoint.position;
            player.transform.eulerAngles = spawnPoint.eulerAngles;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            playerPosition = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = false;
            playerPosition = "";
        }
    }
    void OnEnable()
    {
        PlayerInput.Interact.Enable();
    }
    void OnDisable()
    {
        PlayerInput.Interact.Disable();
    }
}
