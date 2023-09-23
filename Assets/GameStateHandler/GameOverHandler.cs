using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class GameOverHandler : MonoBehaviour
{
    private bool IsGameWon = false;
    public void HandlePlayerReachedObjective(GameObject player)
    {
        if (IsGameWon) return;

        // TODO: Remove this
        Debug.Log($"{player.name} wins");

        IsGameWon = true;
        // Time.timeScale = 0;
    }
}
