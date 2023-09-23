using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoneSwitcher : MonoBehaviour
{
    public string triggerTag;
    private CinemachineFreeLook primaryCamera;
    private CinemachineVirtualCameraBase[] virtualCameras;
    private CinemachineVirtualCameraBase targetCamera;
    void Start()
    {
        setVcams();
        setPrimaryCamera();
        SwitchToCamera(primaryCamera);
    }

    void Update()
    {
        if (primaryCamera == null)
        {
            setPrimaryCamera();
        }
        if (virtualCameras == null || virtualCameras.Length == 0)
        {
            setVcams();
        }
    }

    private void setPrimaryCamera()
    {
        primaryCamera = GameObject.Find("Third person Camera").GetComponent<CinemachineFreeLook>();
    }

    private void setVcams()
    {
        virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCameraBase>();
        foreach (CinemachineVirtualCameraBase camera in virtualCameras)
        {
            camera.LookAt = this.gameObject.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            targetCamera = other.GetComponentInChildren<CinemachineVirtualCameraBase>();
            Invoke("switchcamera", 0.02f);
        }
    }

    private void switchcamera()
    {
        SwitchToCamera(targetCamera);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            SwitchToCamera(primaryCamera);
        }
    }

    private void SwitchToCamera(CinemachineVirtualCameraBase TargetCamera)
    {
        foreach (CinemachineVirtualCameraBase camera in virtualCameras)
        {
            camera.enabled = camera == TargetCamera;
        }
    }

}
