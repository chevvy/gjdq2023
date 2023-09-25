using UnityEngine;
using UnityEngine.AI;

public class RunnerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _objective;
    [SerializeField] private Animator _animator;

    public void StartRunningAnimation()
    {
        _animator.SetTrigger("Run");
    }

    public void StartKnockbackAnimation()
    {
        _animator.SetTrigger("Hurt");
    }
    
    public void StartIdleAnimation()
    {
        _animator.enabled = false;
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _objective = GameObject.FindWithTag(Tags.Objective).transform;

        if (_agent is null)
            Debug.LogError($"[{GetType()}] Missing agent reference");
        if(_objective is null)
            Debug.LogError($"[{GetType()}] Missing objective reference");
        if(_animator is null)
            Debug.LogError($"[{GetType()}] Missing animator reference");

        SetDestination();

        StartRunningAnimation();
    }

    void SetDestination()
    {
        if (_agent.isActiveAndEnabled)
        {
            var destination = new Vector3(
                _objective.transform.position.x, 
                _agent.transform.position.y, 
                _agent.transform.position.z
            );

            _agent.SetDestination(destination);
        }
    }

    void LateUpdate()
    {
        SetDestination();
    }
}
