using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    private Vector3 movementThisFrame = Vector3.zero;
    private Vector3 selectedObjectMovementThisFrame = Vector3.zero; 
    
    public ModelManager ModelManager;

    public Transform frontOfCharacter;

    public float radius = 10.0f;

    private bool _isObjectSelected = false;
    // [CanBeNull] private IPossessable _selectedPossessable;
    [CanBeNull] private GameObject _selectedObject;
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputValues = context.ReadValue<Vector2>();
        movementThisFrame.x = inputValues.x;
        movementThisFrame.y = inputValues.y;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // Release object
        if (_selectedObject != null)
        {
            if (_selectedObject.TryGetComponent(out IPossessable poss))
            {
                var selectedObjectPrefab = poss.GetPrefab();
                // var droppedObj = Instantiate(selectedObjectPrefab,transform.position + new Vector3(0,1,0),Quaternion.identity);
                var droppedObj =      Instantiate (selectedObjectPrefab,
                    transform.position + transform.forward + Vector3.up*0.14f,
                    transform.rotation);
                if (droppedObj.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = false;
                }

                if (_selectedObject != null)
                {
                    Destroy(_selectedObject);
                }
            }

            return;
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            if (!hit.TryGetComponent(out IPossessable possessable)) continue;
            var newPrefab = possessable.GetPrefab();
            
            var newInstance = Instantiate(newPrefab, frontOfCharacter.position, frontOfCharacter.rotation,
                frontOfCharacter);
            _selectedObject = newInstance;
            Destroy(hit.gameObject);
            
            if (newInstance.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            else
            {
                Debug.LogError("NO RB ON NEW COMP");
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 viewVector = context.ReadValue<Vector2>();
        selectedObjectMovementThisFrame.x = viewVector.x;
        selectedObjectMovementThisFrame.z = viewVector.y;
    }

    void Update()
    {
        _inputs = Vector3.zero;
        _inputs.x = movementThisFrame.x;
        _inputs.y = 0.0f;
        _inputs.z = movementThisFrame.y;

        if (_inputs != Vector3.zero)
        {
            _body.AddForce(_inputs);
        }
        
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Transform playerTransform = transform;
            ModelManager.ThrowCurrentModel(playerTransform.forward, playerTransform.rotation);
        }
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        transform.LookAt(transform.position + selectedObjectMovementThisFrame * Time.fixedDeltaTime);
    }
}
