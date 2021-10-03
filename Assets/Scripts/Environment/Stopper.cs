using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    public int score;
    public AudioSource audioSource;
    //public float pitchOffset = 0.2f;

    //private float basePitch;

    //private void Start()
    //{
    //    basePitch = audioSource.pitch;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player)
        {
            GameController.Instance.TargetScore += score;
            //audioSource.pitch = basePitch + Random.Range(-pitchOffset, pitchOffset);
            audioSource.Play();
        }
    }
}
