using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public GameObject interactCanvas;
    RaycastTesting raycastTesting;
    private Camera cam;
    public float baseDistance = 5.0f;
    public float baseScale = 2.0f;
    private Transform textTransform;
    void Start()
    {
        cam = Camera.main;
        raycastTesting = GameObject.Find("RaycastTesting").GetComponent<RaycastTesting>();
        gameObject.GetComponent<Outline>().enabled = false;
        textTransform = interactCanvas.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().transform;
    }

    void Update()
    {
        if (raycastTesting.isInteracting && raycastTesting.lastHitObject == gameObject)
        {
            interactCanvas.transform.LookAt(cam.transform, Vector3.up);
            float distance = Vector3.Distance(cam.transform.position, textTransform.position);
            float scaleFactor = distance / baseDistance;
            textTransform.localScale = (Vector3.one * baseScale * scaleFactor) * 1.6f;
            interactCanvas.SetActive(true);
        }
        else
        {
            interactCanvas.SetActive(false);
        }
    }
}
