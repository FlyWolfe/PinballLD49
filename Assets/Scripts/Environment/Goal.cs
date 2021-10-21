using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of the goal area and calls the necessary methods to win the game
/// </summary>
public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // Goals can only collide with players, so this is a win
        GameController.Instance.WinGame();
    }
}
