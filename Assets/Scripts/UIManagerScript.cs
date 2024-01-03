using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public GameObject InteractPanel;
    public GameObject CheckWardrobe;
    public string locationName;
    public TextMeshProUGUI locationText;
    public static bool playerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = true;
            if (gameObject.name == "Wardrobe" && HandleProgress.currentObjectiveIndex < 6)
            {
                CheckWardrobe.SetActive(true);
                InteractPanel.SetActive(false);
            }
            else if (gameObject.name != "Wardrobe")
            {
                InteractPanel.SetActive(true);
                locationText.text = locationName;
            }
            else
            {
                if (gameObject.name == "Wardrobe")
                {
                    CheckWardrobe.SetActive(true);
                    InteractPanel.SetActive(true);
                }
                else
                {
                    InteractPanel.SetActive(true);
                    locationText.text = locationName;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = false;
            InteractPanel.SetActive(false);
            CheckWardrobe.SetActive(false);
            locationText.text = "";
        }
    }
}



