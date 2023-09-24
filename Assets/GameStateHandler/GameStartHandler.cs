using NUnit.Framework;
using UnityEngine;

public class GameStartHandler : MonoBehaviour
{
    public GameObject[] CharacterPrefabs;
    private GameObject[] _spawnPoints;
    private GameObject[] _players;

    void Start()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag(Tags.RunnerSpawnPoint);
        _players = GameObject.FindGameObjectsWithTag(Tags.Player);

        Assert.True(_players.Length <= _spawnPoints.Length);

        for (int i = 0; i < _players.Length; i++)
        {
            InstantiateRunner(i, _spawnPoints[i], CharacterPrefabs[i]);
        }
    }

    void InstantiateRunner(int index, GameObject spawnPoint, GameObject prefab)
    {
        Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
