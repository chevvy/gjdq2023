using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrefabManager : MonoBehaviour
{
    public Material[] _playerMaterial;

    public void onPlayerJoin(PlayerInput input)
    {
        input.GameObject().GetComponent<MeshRenderer>().material = _playerMaterial[input.playerIndex];
    }
}
