using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : MonoBehaviour
{
    
    public Animator animator;

    private void OnMouseDown() {
        animator.SetTrigger("Push");
    }
}
