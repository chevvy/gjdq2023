using System;
using UnityEngine;

public class GameStartHandler : MonoBehaviour
{
    public GameObject[] CharacterPrefabs;
    private GameObject[] _spawnPoints;
    private GameObject[] _players;

    private void Start()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag(Tags.RunnerSpawnPoint);
        _players = GameObject.FindGameObjectsWithTag(Tags.Player);

        for (int i = 0; i < _players.Length; i++)
        {
            InstantiateRunner(i, _spawnPoints[i], CharacterPrefabs[i]);
        }

        CrownPreviousGameWinner(GameObject.Find(ScenePersistentData.PreviousGame.Winner));
    }

    void InstantiateRunner(int index, GameObject spawnPoint, GameObject prefab)
    {
        Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
    }

    void CrownPreviousGameWinner(GameObject runner)
    {
        if (null == runner) return;

        runner.transform.Find("Crown").gameObject.SetActive(true);
    }
}
