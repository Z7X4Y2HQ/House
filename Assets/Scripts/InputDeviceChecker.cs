using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceChecker : MonoBehaviour
{
    public InputActionAsset inputActions;
    private int actionCount;
    private InputAction[] actions;

    private void Awake()
    {
        actionCount = 0;
        foreach (var actionMap in inputActions.actionMaps)
        {
            actionCount += actionMap.actions.Count;
        }
        actions = new InputAction[actionCount];

        int index = 0;
        foreach (var actionMap in inputActions.actionMaps)
        {
            foreach (var action in actionMap)
            {
                actions[index++] = action;
            }
        }
    }

    private void OnEnable()
    {
        foreach (var action in actions)
        {
            action.started += OnActionStarted;
            action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var action in actions)
        {
            action.started -= OnActionStarted;
            action.Disable();
        }
    }

    private void OnActionStarted(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        if (device is Keyboard || device is Mouse)
        {
            Debug.Log("Keyboard or Mouse used");
        }
        else if (device is Gamepad)
        {
            Debug.Log("Gamepad used");
        }
    }
}