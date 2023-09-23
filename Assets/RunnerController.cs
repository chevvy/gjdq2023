using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RunnerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameOverHandler _gameOverHandler; // To notify the agent has won the game
    [SerializeField] private Transform _objective; // The position the agent is pursuing

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed *= Random.Range(0.25f, 2.0f); // TODO: Remove
        _gameOverHandler = GameObject.Find("GameStateManager").GetComponent<GameOverHandler>();
        
        Assert.NotNull(_agent);
        Assert.NotNull(_objective);
        Assert.NotNull(_gameOverHandler);

        SetDestination();
    }

    void Update()
    {
        if (IsDestinationReached())
            _gameOverHandler.HandlePlayerReachedObjective(gameObject);
    }

    bool IsDestinationReached()
    {
        return transform.position.x == _objective.transform.position.x
            && transform.position.z == _objective.transform.position.z;
    }

    void SetDestination()
    {
        if (_agent.isActiveAndEnabled)
            _agent.SetDestination(_objective.position);
    }
}
