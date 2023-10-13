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

        _rigidBody.AddForce(direction * force, ForceMode.Force);

        yield return new WaitForSeconds(delay);
        
        _agent.enabled = true;
        _agent.SetDestination(destination);
        _runnerAnimator.StartRunningAnimation();
    }
}
