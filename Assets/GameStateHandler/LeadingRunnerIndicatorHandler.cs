using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class LeadingRunnerIndicatorHandler : MonoBehaviour
{
    private GameObject[] runners;

    public void Start()
    {
        runners = GameObject.FindGameObjectsWithTag(Tags.Runner);
        Assert.NotNull(runners);
        Assert.IsNotEmpty(runners);
    }

    public void LateUpdate()
    {
        runners = runners.OrderByDescending(x => x.transform.position.x).ToArray();
        IndicateRunnerAsRunLeader(runners[0]);
        IndicateRunnersAsRunnerUp(runners[1..]);
    }

    private void IndicateRunnerAsRunLeader(GameObject runner)
    {
        runner.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
    }

    private void IndicateRunnersAsRunnerUp(GameObject[] runners)
    {
        foreach (var player in runners)
            player.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
    }
}
