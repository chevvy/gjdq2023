using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PartyPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
    
        private float _moveByXThisFrame = 0f;
        private float _moveByYThisFrame = 0f;
        [FormerlySerializedAs("PlayerInput")] public PlayerInput playerInput;
    
        public Material[] materials;
        public MeshRenderer playerMeshRenderer;
    
        private Vector3 _velocity;
        private bool isGrounded;

        public Possesser Possesser;
        public ModelManager ModelManager;
        private void Start()
        {
            if (controller == null)
            {
                Debug.LogError("MISSING char controller on party player");
            }

            if (materials.Length < 3)
            {
                Debug.LogError("Missing player materials on player controller");
            }
            try
            {
                Material currentPlayerMaterial = materials[playerInput.playerIndex];
                playerMeshRenderer.material = currentPlayerMaterial;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (Possesser == null)
            {
                Debug.LogError("Missing possesser script on player");
            }
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            GameObject newModel = Possesser.PossessNearestItem();
            if (newModel != null)
            {
                ModelManager.SetModel(newModel);
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
        }
}
