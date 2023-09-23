using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class LeadingRunnerIndicatorHandler : MonoBehaviour
{
    private GameObject[] players;

    public void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Assert.NotNull(players);
        Assert.IsNotEmpty(players);
    }

    public void LateUpdate()
    {
        players = players.OrderByDescending(x => x.transform.position.x).ToArray();
        IndicatePlayerAsRunLeader(players[0]);
        IndicatePlayersAsRunnerUp(players[1..]);
    }

    private void IndicatePlayerAsRunLeader(GameObject player)
    {
        player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
    }

    private void IndicatePlayersAsRunnerUp(GameObject[] players)
    {
        foreach (var player in players)
            player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
    }
}
