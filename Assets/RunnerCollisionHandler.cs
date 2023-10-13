using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RunnerCollisionHandler : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Rigidbody _rigidBody;
    public RunnerAnimator _runnerAnimator;
    [SerializeField] private float KnockbackDelay = 0.5f;
    [SerializeField] private float KnockbackForce = 10.0f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        _runnerAnimator = GetComponent<RunnerAnimator>();

        if(_agent is null)
            Debug.LogError($"[{GetType()}] Missing agent reference");
        if(_rigidBody is null)
            Debug.LogError($"[{GetType()}] Missing rigidbody reference");
        if(_runnerAnimator is null)
            Debug.LogError($"[{GetType()}] Missing RunnerAnimator reference");
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (!CollidedWithObstacle(collision)) return;
    //
    //     if (collision.gameObject.CompareTag("Player"))
    //     { 
    //         if (collision.gameObject.TryGetComponent(out PartyRigidbodyController controller))
    //         {
    //             if (!controller.isObjectSelected) return;
    //
    //             controller.DropAndDestroy();
    //         }
    //     }
    //     else
    //     {
    //         Destroy(collision.gameObject);
    //     }
    //
    //     var knockbackDirection = collision.contacts[0].normal;
    //
    //     StartCoroutine(KnockbackAgent(KnockbackDelay, knockbackDirection, KnockbackForce));
    // }

    public void StartKnockBack(Vector3 knockbackDirection)
    {
        StartCoroutine(KnockbackAgent(KnockbackDelay, knockbackDirection, KnockbackForce));
    }
    
    IEnumerator KnockbackAgent(float delay, Vector3 direction, float force)
    {
        
        if(_agent is null)
            Debug.LogError($"[{GetType()}] Missing agent reference");
        if(_rigidBody is null)
            Debug.LogError($"[{GetType()}] Missing rigidbody reference");

        var destination = _agent.destination;
        _runnerAnimator.StartKnockbackAnimation();
        _agent.enabled = false;
        // _rigidBody.isKinematic = false;

        _rigidBody.AddForce(direction * force, ForceMode.Force);

        yield return new WaitForSeconds(delay);

        // _rigidBody.isKinematic = true;
        _agent.enabled = true;
        _agent.SetDestination(destination);
        _runnerAnimator.StartRunningAnimation();
    }
    
    // bool CollidedWithObstacle(Collision collision)
    // {
    //     return collision.gameObject.CompareTag(Tags.Obstacle) || collision.gameObject.CompareTag("Player");
    // }
}
