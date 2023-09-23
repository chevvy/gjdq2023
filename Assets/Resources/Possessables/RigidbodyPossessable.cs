using System.Collections;
using System.Collections.Generic;
using Resources.Player;
using UnityEngine;

public class RigidbodyPossessable : MonoBehaviour, IPossessable
{
    [SerializeField] private GameObject possessablePrefab;

    public GameObject GetPrefab() => possessablePrefab;

    public void PossessItem()
    {
        Destroy(gameObject);
    }
}
