using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class DoorTrigger : MonoBehaviour
{
    PlayerInput PlayerInput;
    [SerializeField]
    private Door door;
    private bool playerInRange = false;

    void Awake()
    {
        PlayerInput = new PlayerInput();
    }
    void Update()
    {
        if (!door.isRotatingDoor)
        {
            if (playerInRange && PlayerInput.Interact.InteractObject.triggered && !door.isOpen)
            {
                door.Open(transform.position);
            }
            else if (playerInRange && PlayerInput.Interact.InteractObject.triggered && door.isOpen)
            {
                door.Close();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = true;
            if (door.isRotatingDoor && !door.isOpen)
            {
                door.Open(other.transform.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            playerInRange = false;
            if (door.isRotatingDoor && door.isOpen)
            {
                door.Close();
            }
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
