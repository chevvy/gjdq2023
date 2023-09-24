using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.Runner))
        {
            HandlePlayerReachedObjective(collider.gameObject);
        }
    }

    public void HandlePlayerReachedObjective(GameObject runner)
    {
        Debug.Log($"{runner.name} wins");

        runner.GetComponent<RunnerController>().StartIdleAnimation();

        SceneManager.LoadScene(SceneIndex.EndGameScene);
    }
}
