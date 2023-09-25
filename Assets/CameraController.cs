using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Runner 1");

        // Assert.NotNull(_player);
    }

    void LateUpdate()
    {
        var position = transform.position;

        position.x = _player.transform.position.x;

        transform.position = position;
    }
}
