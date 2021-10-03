using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public float bounceStrength;
    public Animator animator;
    public int score = 100;
    public AudioSource audioSource;
    public AudioSource audioSourceHit;

    private void OnCollisionEnter(Collision other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player) {
            player.AddForceImpulse(transform.forward.normalized, bounceStrength);
            animator.SetTrigger("Bump");
            GameController.Instance.TargetScore += score;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
            audioSourceHit.Play();
        }
    }
}
