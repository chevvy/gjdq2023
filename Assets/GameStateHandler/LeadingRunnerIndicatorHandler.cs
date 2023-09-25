using System.Linq;
using UnityEngine;

public class LeadingRunnerIndicatorHandler : MonoBehaviour
{
    private GameObject[] runners;

    public void Start()
    {
        runners = GameObject.FindGameObjectsWithTag(Tags.Runner);
        // Assert.NotNull(runners);
        // Assert.IsNotEmpty(runners);
    }

    public void LateUpdate()
    {
        runners = runners.OrderByDescending(x => x.transform.position.x).ToArray();
        
        if (runners[0].transform.position.x < -23) return; // before the start line do not handle crowning players

        IndicateRunnerAsRunLeader(runners[0]);
        IndicateRunnersAsRunnerUps(runners[1..]);
    }

    private void IndicateRunnerAsRunLeader(GameObject runner)
    {
        runner.transform.Find("Crown").gameObject.SetActive(true);
    }

    private void IndicateRunnersAsRunnerUps(GameObject[] runners)
    {
        foreach (var runner in runners)
            runner.transform.Find("Crown").gameObject.SetActive(false);
    }
}
