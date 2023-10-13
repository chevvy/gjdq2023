using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Runner 1");

        if(_player is null)
            Debug.LogError($"[{GetType()}] couldnt find player 1");
    }

    void LateUpdate()
    {
        var position = transform.position;

        position.x = _player.transform.position.x;

        transform.position = position;
    }
}
