using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    public enum PlayerEvents
    {
        Hover,
        NotHover,
        Interact
    }
    
    private Dictionary<PlayerEvents, UnityEvent<GameObject>> _events;

    private static PlayerEventManager _eventManager;

    public static PlayerEventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType<PlayerEventManager>();
                if (!_eventManager)
                {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    _eventManager.Init();
                }
            }

            return _eventManager;
        }
    }

    void Init()
    {
        if (_events == null)
        {
            _events = new Dictionary<PlayerEvents, UnityEvent<GameObject>>();
        }
    }
    
    public static void StartListening (PlayerEvents eventName, UnityAction<GameObject> listener)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (instance._events.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
            thisEvent = new UnityEvent<GameObject>();
            thisEvent.AddListener (listener);
            instance._events.Add (eventName, thisEvent);
        }
    }
    
    public static void StopListening (PlayerEvents eventName, UnityAction<GameObject> listener)
    {
        if (_eventManager == null) return;
        UnityEvent<GameObject> thisEvent = null;
        if (instance._events.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }
    
    public static void TriggerEvent (PlayerEvents eventName, GameObject relatedObject)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (instance._events.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke(relatedObject);
        }
    }
}
