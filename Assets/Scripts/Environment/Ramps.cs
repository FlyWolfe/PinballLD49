using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramps : MonoBehaviour
{
    public float speedUpForce;

    public AudioSource audioSource;

    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            player.AddForce(transform.forward, speedUpForce);
        }
    }

}
