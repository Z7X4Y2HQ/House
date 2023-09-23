using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTesting : MonoBehaviour
{
    private Camera cam;
    private int layerMask;
    public GameObject lastHitObject;
    private int InteractableLayer;
    public float radius = 0.5f;
    public bool isInteracting = false;

    void Start()
    {
        cam = Camera.main;
        InteractableLayer = LayerMask.NameToLayer("Interactable");
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
}
