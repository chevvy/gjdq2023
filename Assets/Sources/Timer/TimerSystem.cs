using TMPro;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] private float levelDuration = 15;
    [SerializeField] public GameObject timerGameObject;
    private TextMeshProUGUI _timerText;

    private bool _shouldValidateTime = true;

    private void Start()
    {
        if (timerGameObject == null)
        {
            Debug.LogError("[TimerSystem] Missing reference to timerText text mesh pro");
        }

        var timerText = timerGameObject.GetComponent<TextMeshProUGUI>();
        if (timerText != null)
        {
            _timerText = timerText;
        }
        else
        {
            Debug.LogError("[TimerSystem] Couldnt fetch TextMeshPro object");
        }
    }

    private void Update()
    {
        if (_timerText == null)
        {
            return;
        }
        levelDuration -= Time.deltaTime;
        if (levelDuration < 0 && _shouldValidateTime)
        {
            Debug.Log("GAME OVER");
            _shouldValidateTime = false;
            return;
        }

        if (_shouldValidateTime)
        {
            _timerText.text = levelDuration.ToString("f2");
        }
    }
}
