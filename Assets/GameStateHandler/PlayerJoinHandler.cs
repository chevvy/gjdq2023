using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput) 
    {
        var player = playerInput.gameObject;

        player.transform.position = new Vector3(
            player.transform.position.x,
            1.25f,
            player.transform.position.z      
        );
    }
}
