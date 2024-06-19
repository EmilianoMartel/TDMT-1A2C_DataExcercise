using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventChannel;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string runActionName = "Run";

    [SerializeField] private Vector2Channel _moveEvent;
    [SerializeField] private BoolChannel _runEvent;

    private void OnEnable()
    {
        var moveAction = inputActions.FindAction(moveActionName);
        if (moveAction != null)
        {
            moveAction.performed += HandleMoveInput;
            moveAction.canceled += HandleMoveInput;
        }
        var runAction = inputActions.FindAction(runActionName);
        if (runAction != null)
        {
            runAction.started += HandleRunInputStarted;
            runAction.canceled += HandleRunInputCanceled;
        }
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        //TODO DONE: Implement event logic
        Debug.Log($"{name}: Run input started");
        _moveEvent.InvokeEvent(ctx.ReadValue<Vector2>());
    }

    private void HandleRunInputCanceled(InputAction.CallbackContext ctx)
    {
        //TODO DONE: Implement event logic
        Debug.Log($"{name}: Run input canceled");
        _runEvent.InvokeEvent(false);
    }

    private void HandleRunInputStarted(InputAction.CallbackContext ctx)
    {
        //TODO DONE: Implement event logic
        _runEvent.InvokeEvent(false);
    }
}
