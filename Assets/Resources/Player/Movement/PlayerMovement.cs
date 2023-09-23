using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 _velocity;
    private bool isGrounded;

    public LayerMask interactableLayer;

    public Camera playerCamera;

    private float _moveByXThisFrame = 0f;
    private float _moveByYThisFrame = 0f;

    public AudioClips playerWalkSfx;

    public float distanceBeforeObjectIsInteractable = 5f;

    private void Start()
    {
        _velocity = new Vector3();
        if (playerCamera == null)
        {
            Debug.LogError("Missing camera reference on PlayerMovement");
        }

        if (playerWalkSfx == null)
        {
            Debug.LogError("Missing player sfx ");
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        var localTransform = transform;
        Vector3 move = localTransform.right * _moveByXThisFrame + localTransform.forward * _moveByYThisFrame;

        controller.Move(move * speed * Time.deltaTime);
        
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveVec = context.ReadValue<Vector2>();
        _moveByXThisFrame = moveVec.x;
        _moveByYThisFrame = moveVec.y;
        
        if (context.performed)
        {
            AudioManager.Instance.StartPlayingWalkSfx(playerWalkSfx.audioClips, playerWalkSfx.audioSource);
        } else if (context.canceled)
        {
            AudioManager.Instance.StopPlayingWalkSfx(playerWalkSfx.audioSource);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void FixedUpdate()
    {
        Transform cameraTransform = playerCamera.transform;
        if (Physics.Raycast(
                cameraTransform.position, 
                cameraTransform.TransformDirection(Vector3.forward), 
                out var hit, 
                distanceBeforeObjectIsInteractable, 
                interactableLayer))
        {
            // Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                PlayerEventManager.TriggerEvent(PlayerEventManager.PlayerEvents.Hover, hit.collider.gameObject);
            }
        }
        else
        {
            // Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * 1000, Color.white);
            PlayerEventManager.TriggerEvent(PlayerEventManager.PlayerEvents.NotHover, gameObject);
        }
        
    }
}
