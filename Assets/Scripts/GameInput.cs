using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public event EventHandler OnInputAction;
    public event EventHandler OnInteractAlternate;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputvector = inputActions.Player.Move.ReadValue<Vector2>();
        inputvector = inputvector.normalized;
        return inputvector;
    }
}
