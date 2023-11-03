using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationMovementController : MonoBehaviour
{
    PlayerInput PlayerInput;
    CharacterController characterController;
    public static Animator animator;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 7f;
    int isWalkHash;
    private new Transform camera;
    private CinemachineFreeLook primaryCamera;
    Vector3 moveDirection;
    public float gravity = 9.8f;
    private float verticalVelocity = 0.0f;
    private PhoneManager phoneManager;



    void Awake()
    {
        PlayerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        phoneManager = GameObject.Find("PhoneManager").GetComponent<PhoneManager>();
        animator = GetComponent<Animator>();

        isWalkHash = Animator.StringToHash("isWalk");

        PlayerInput.Movement.MovePlayer.started += onMovementInput;
        PlayerInput.Movement.MovePlayer.performed += onMovementInput;
        PlayerInput.Movement.MovePlayer.canceled += onMovementInput;
    }

    void Update()
    {
        if (camera == null)
        {
            camera = GameObject.Find("Main Camera").transform;
        }

        if (primaryCamera == null)
        {
            primaryCamera = GameObject.Find("Third person Camera").GetComponent<CinemachineFreeLook>();
            primaryCamera.LookAt = transform;
            primaryCamera.Follow = transform;
        }

        handleGravity();
        if (!DialogueManager.dialogueIsPlaying && !phoneManager.phoneOut)
        {
            handleAnimation();
            handleRotation();
        }


        if (isMovementPressed && !DialogueManager.dialogueIsPlaying && !phoneManager.phoneOut)
        {
            characterController.Move(moveDirection * Time.deltaTime * 1.5f);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(moveVector * Time.deltaTime);
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleRotation()
    {
        Vector3 cameraForward = camera.forward;
        Vector3 cameraRight = camera.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        moveDirection = cameraForward * currentMovement.z + cameraRight * currentMovement.x;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }



    void handleAnimation()
    {
        bool isWalk = animator.GetBool(isWalkHash);

        if (isMovementPressed && !isWalk)
        {
            animator.SetBool(isWalkHash, true);
        }
        else if (!isMovementPressed && isWalk)
        {
            animator.SetBool(isWalkHash, false);
        }
    }

    void OnEnable()
    {
        PlayerInput.Movement.Enable();
    }
    void OnDisable()
    {
        PlayerInput.Movement.Disable();
    }
}
