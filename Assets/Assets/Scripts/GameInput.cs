using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private static GameInput _instance;
    public static GameInput Instance {  get { return _instance; } }

    public event EventHandler OnInteractAction;


    private PlayerInputActions playerInputActions;
    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerInputActions = new PlayerInputActions();
        OnEnable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetPlayerMovement()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();



        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMouseDelta()
    {
        Vector2 mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>();

        return mouseDelta;
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerInputActions.Player.Jump.triggered;
    }

}
