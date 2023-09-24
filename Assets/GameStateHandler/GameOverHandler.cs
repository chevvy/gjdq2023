using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.Runner))
            HandlePlayerReachedObjective(collider.gameObject);
    }

    public void HandlePlayerReachedObjective(GameObject runner)
    {
        runner.GetComponent<RunnerController>().StartIdleAnimation();

        SceneManager.LoadScene(SceneIndex.GameScene);
    }
}
