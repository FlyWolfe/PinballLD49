using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    public int score;
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player)
        {
            GameController.Instance.TargetScore += score;
            audioSource.Play();
        }
    }
}
