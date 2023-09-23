using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public GameObject InteractPanel;
    public string locationName;
    public TextMeshProUGUI locationText;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            InteractPanel.SetActive(true);
            locationText.text = locationName;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            InteractPanel.SetActive(false);
            locationText.text = "";
        }
    }

}

