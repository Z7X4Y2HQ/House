using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool playerInRange;
    public static string currentCollidingNPCName;

    void Awake()
    {
        currentCollidingNPCName = null;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "NPC" || collider.gameObject.tag == "Dialogue Trigger")
        {
            currentCollidingNPCName = collider.gameObject.name;
            playerInRange = true;
            Debug.Log("Player in range of " + collider.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "NPC" || collider.gameObject.tag == "Dialogue Trigger")
        {
            playerInRange = false;
            Debug.Log("Player not in range");
            currentCollidingNPCName = null;
        }
    }
}
