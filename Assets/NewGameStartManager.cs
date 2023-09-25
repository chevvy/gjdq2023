using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NewGameStartManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] _spawnPoints;
    private GameObject[] _players;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ON START");  
        
        _spawnPoints = GameObject.FindGameObjectsWithTag(Tags.RunnerSpawnPoint);
        _players = GameObject.FindGameObjectsWithTag(Tags.Player);
        

        for (int i = 0; i < _players.Length; i++)
        {
            InstantiateRunner(i, _spawnPoints[i], characterPrefabs[i]);
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
