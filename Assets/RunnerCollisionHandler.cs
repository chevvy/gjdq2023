using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RunnerCollisionHandler : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Rigidbody _rigidBody;
    [SerializeField] private float KnockbackDelay = 0.5f;
    [SerializeField] private float KnockbackForce = 100f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();

        Assert.NotNull(_agent);
        Assert.NotNull(_rigidBody);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!CollidedWithObstacle(collision)) return;

        Destroy(collision.gameObject);

        StartCoroutine(KnockbackAgent(KnockbackDelay, collision.contacts[0].normal, KnockbackForce));
    }
    
    IEnumerator KnockbackAgent(float delay, Vector3 direction, float force)
    {
        Assert.NotNull(_agent);
        Assert.NotNull(_rigidBody);

        var destination = _agent.destination;
        _agent.enabled = false;
        _rigidBody.isKinematic = false;

        _rigidBody.AddForce(direction * force, ForceMode.Impulse);

        yield return new WaitForSeconds(delay);

        _rigidBody.isKinematic = true;
        _agent.enabled = true;
        _agent.SetDestination(destination);
    }
    
    bool CollidedWithObstacle(Collision collision)
    {
        return collision.gameObject.CompareTag(Tags.Obstacle);
    }
}
