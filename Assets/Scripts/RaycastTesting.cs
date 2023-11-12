using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RaycastTesting : MonoBehaviour
{
    private Camera cam;
    PlayerInput PlayerInput;
    private int layerMask;
    public GameObject lastHitObject;
    private int InteractableLayer;
    public float radius = 0.5f;
    public bool isInteracting = false;
    private HandleProgress handleProgress;

    void Awake()
    {
        PlayerInput = new PlayerInput();
    }
    void Start()
    {
        cam = Camera.main;
        InteractableLayer = LayerMask.NameToLayer("Interactable");
        handleProgress = GameObject.Find("HandleHUD").GetComponent<HandleProgress>();
        layerMask = 1 << InteractableLayer;
    }

    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = cam.ScreenPointToRay(screenCenter);

        RaycastHit hit;
        if (Physics.SphereCast(ray, radius, out hit, 2f, layerMask))
        {
            if (hit.collider.gameObject.layer == InteractableLayer)
            {
                if (lastHitObject != null && lastHitObject != hit.collider.gameObject)
                {
                    setOutline(false);
                    isInteracting = false;
                }
                lastHitObject = hit.collider.gameObject;
                setOutline(true);
                isInteracting = true;
                if (PlayerInput.Interact.InteractObject.triggered)
                {
                    if (hit.collider.gameObject.name == "Phone")
                    {
                        if (handleProgress.objectives[0].isCompleted)
                        {
                            // Destroy(hit.collider.gameObject);
                            hit.collider.gameObject.SetActive(false);
                            HandleProgress.pickedUpPhone = true;
                        }
                    }
                    else if (hit.collider.gameObject.name == "Knife")
                    {
                        // if (HandleProgress.currentObjectiveIndex == 4)
                        // {
                        hit.collider.gameObject.SetActive(false);
                        HandleProgress.pickedUpKnife = true;
                        HandleProgress.currentObjectiveIndex = 4; //Tempppp
                        HandleProgress.currentScene = "Chapter_one_talking_to_mom"; //Tempppp
                        // }
                    }
                }

            }
            else if (lastHitObject != null)
            {
                setOutline(false);
                isInteracting = false;
                lastHitObject = null;
            }
        }
        else if (lastHitObject != null)
        {
            setOutline(false);
            isInteracting = false;
            lastHitObject = null;
        }
    }

    private void setOutline(bool outlineBool)
    {
        lastHitObject.GetComponent<Outline>().enabled = outlineBool;
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
