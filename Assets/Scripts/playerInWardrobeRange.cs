using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInWardrobeRange : MonoBehaviour
{
    public static bool changeClothes = false;
    public static string currentCloths;

    void Awake()
    {
        if (currentCloths == null)
        {
            currentCloths = PlayerPrefs.GetString("currentCharacter");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            SceneManagerScript.currentCharacter = other.gameObject.tag;
            PlayerPrefs.SetString("currentCharacter", other.gameObject.tag);
            currentCloths = other.gameObject.tag;
            changeClothes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            PlayerPrefs.SetString("currentCharacter", other.gameObject.tag);
            SceneManagerScript.currentCharacter = other.gameObject.tag;
            currentCloths = other.gameObject.tag;
            changeClothes = false;
        }
    }
}
