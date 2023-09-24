using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private float EndGameDelay = 2.0f;
    private bool _isEnding = false;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.Runner))
            HandlePlayerReachedObjective(collider.gameObject);
    }

    public void HandlePlayerReachedObjective(GameObject runner)
    {
        runner.GetComponent<RunnerController>().StartIdleAnimation();

        if (!_isEnding)
        {
            StartCoroutine(BeginEndGameTransition(EndGameDelay));
        }
    }

    IEnumerator BeginEndGameTransition(float t)
    {
        _isEnding = true;

        yield return new WaitForSeconds(t);

        SceneManager.LoadScene(SceneIndex.GameScene);
    }
}
