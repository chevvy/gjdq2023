using System.Collections;
using System.Collections.Generic;
using Resources.Player;
using UnityEngine;

public class RigidbodyPossessable : MonoBehaviour, IPossessable
{
    [SerializeField] private GameObject possessablePrefab;

    public GameObject GetPrefab() => possessablePrefab;
    
    public AudioClip _sfxOnDestroy;
    

    public void PossessItem()
    {
        Destroy(gameObject);
    }

    public AudioClip sfxOnDestroy()
    {
        return _sfxOnDestroy;
    }
}
