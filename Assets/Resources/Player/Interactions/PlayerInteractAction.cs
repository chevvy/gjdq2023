using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractAction : MonoBehaviour
{
    private IInteractable _interactableObject;
    private GameObject _selectedGameObjectReference;

    private UnityAction<GameObject> _onHoverListener;
    private UnityAction<GameObject> _notOnHoverListener;

    public Material OnHoverMaterial;
    public Material NotOnHoverMaterial;
    

    private void Awake()
    {
        _onHoverListener = OnHover;
        _notOnHoverListener = OnQuitHover;
    }

    private void OnEnable()
    {
        PlayerEventManager.StartListening(PlayerEventManager.PlayerEvents.Hover, _onHoverListener);
        PlayerEventManager.StartListening(PlayerEventManager.PlayerEvents.NotHover, _notOnHoverListener);
    }

    private void OnDisable()
    {
        PlayerEventManager.StopListening(PlayerEventManager.PlayerEvents.Hover, _onHoverListener);
        PlayerEventManager.StopListening(PlayerEventManager.PlayerEvents.NotHover, _notOnHoverListener);
    }

    public void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (context.performed && _interactableObject != null)
        {
            _interactableObject.Interact();
        }
    }

    private void OnHover(GameObject gameobj)
    {
        if (gameobj.TryGetComponent(out IInteractable interactable))
        {
            _interactableObject = interactable;
            _selectedGameObjectReference = gameobj;
            gameobj.GetComponent<MeshRenderer>().material = OnHoverMaterial;
        }    
    }

    private void OnQuitHover(GameObject latestObject)
    {
        _interactableObject = null;
        if (_selectedGameObjectReference != null && _selectedGameObjectReference.TryGetComponent(out MeshRenderer renderer))
        {
            renderer.material = NotOnHoverMaterial;
        }
    }
}
