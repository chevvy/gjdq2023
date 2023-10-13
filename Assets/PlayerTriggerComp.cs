using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerComp : MonoBehaviour
{
    [SerializeField] private float KnockbackDelay = 0.5f;
    [SerializeField] private float KnockbackForce = 10.0f;

    [SerializeField]
    private RunnerCollisionHandler handler;
    private void OnTriggerEnter(Collider other)
    {
        
        if (!CollidedWithObstacle(other)) return;

        if (other.gameObject.CompareTag("Player"))
        { 
            if (other.gameObject.TryGetComponent(out PartyRigidbodyController controller))
            {
                if (!controller.isObjectSelected) return;

                controller.DropAndDestroy();
            }
        }
        else
        {
            // if (!other.gameObject.CompareTag("Obstacle"))
            // {
            //     Destroy(other.gameObject);
            // }
        }

        var direction = other.transform.position - transform.position;
        handler.StartKnockBack(direction);
    }
    
    bool CollidedWithObstacle(Collider collider)
    {
        return collider.gameObject.CompareTag(Tags.Obstacle) || collider.gameObject.CompareTag("Player");
    }
}
