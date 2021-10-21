using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : MonoBehaviour
{
    /// <summary>
    /// The Animator to play the button push animation when clicking on the button
    /// </summary>
    public Animator animator;

    private void OnMouseDown() {
        animator.SetTrigger("Push");
    }
}
