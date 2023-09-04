using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlock : MonoBehaviour, IInteractable
{
    [SerializeField] 
    public GameObject destroyableResult;

    public Transform explosionPosition;
    
    public float radius = 10.0F;
    public float power = 300.0F;

    public void Interact()
    {
        HandleExplosion();
    }

    private void HandleExplosion()
    {
        var originalBlockPosition = transform.position;
        // we delete the old object before instantiating as to not have any physics issues
        Destroy(gameObject);

        Instantiate(destroyableResult, originalBlockPosition, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(originalBlockPosition, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(power, explosionPosition.position, radius);
            }
        }
    }
}
