using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public GameObject InteractPanel;
    public GameObject CheckWardrobe;
    public GameObject openDoor;
    public string locationName;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI doorText;
    public static bool playerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = true;
            if (gameObject.name == "Door Trigger")
            {
                openDoor.SetActive(true);
                doorText.text = "Interact with Door";
            }
            else if (gameObject.name == "Wardrobe" && HandleProgress.currentObjectiveIndex < 6)
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


    public void WalkOutOfWardrobe()
    {
        playerInRange = false;
        InteractPanel.SetActive(false);
        CheckWardrobe.SetActive(false);
        openDoor.SetActive(false);
        locationText.text = "";
        doorText.text = "Open Wardrobe";
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            WalkOutOfWardrobe();
        }
    }
}



