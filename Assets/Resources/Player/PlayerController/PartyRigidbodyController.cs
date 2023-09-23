using System.Collections;
using System.Collections.Generic;
using Resources.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PartyRigidbodyController : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    public Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    public Transform _groundChecker;

    private Vector3 movementThisFrame = Vector3.zero;
    
    public Possesser Possesser;
    public ModelManager ModelManager;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputValues = context.ReadValue<Vector2>();
        movementThisFrame.x = inputValues.x;
        movementThisFrame.y = inputValues.y;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
            
        GameObject newModel = Possesser.PossessNearestItem();
        Debug.Log("PLAYER: TRIED POSSESSING", newModel);
        if (newModel != null)
        {
            ModelManager.SetModel(newModel);
            _body = ModelManager.GetRigidBody();
        }
    }

    void Update()
    {
        // _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        
        _inputs = Vector3.zero;
        _inputs.x = movementThisFrame.x;
        _inputs.y = 0.0f;
        _inputs.z = movementThisFrame.y;
        
        if (_inputs != Vector3.zero)
            _body.AddForce(_inputs);
            // _body.MovePosition(_inputs);
        
        // if (Input.GetButtonDown("Dash"))
        // {
        //     Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
        //     _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        // }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
        _body.AddForce(dashVelocity, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}
