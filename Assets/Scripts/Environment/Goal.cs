using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // Goals can only collide with players, so this is a win
        GameController.Instance.WinGame();
    }
}
