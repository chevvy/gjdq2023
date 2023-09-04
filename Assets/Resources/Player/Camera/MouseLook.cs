using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float _xRotation = 0f;

    private float xThisFrame = 0f;
    private float yThisFrame = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _xRotation -= yThisFrame;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * xThisFrame);
    }

    public void OnViewMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 val = context.ReadValue<Vector2>();
            xThisFrame = val.x;
            yThisFrame = val.y;
        }
        else
        {
            xThisFrame = 0f;
            yThisFrame = 0f;
        }
    }
}
