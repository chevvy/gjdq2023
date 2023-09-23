using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RunnerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameOverHandler _gameOverHandler;
    [SerializeField] private Transform _objective;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gameOverHandler = GameObject.Find("GameStateManager").GetComponent<GameOverHandler>();
        
        Assert.NotNull(_objective);
        Assert.NotNull(_gameOverHandler);

        SetDestination();
    }

    void Update()
    {
        if (IsDestinationReached())
            _gameOverHandler.HandlePlayerReachedObjective(gameObject);

        _agent.acceleration *= Random.Range(0.8f, 1.2f);
    }

    bool IsDestinationReached()
    {
        return transform.position.x == _objective.transform.position.x
            && transform.position.z == _objective.transform.position.z;
    }

    void SetDestination()
    {
        _agent.SetDestination(_objective.position);
    }
}
