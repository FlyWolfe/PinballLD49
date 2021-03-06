using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    public int score;
    public AudioSource audioSource;
    public float maxImpactForSound = 50f;
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
            
            audioSource.PlayOneShot(audioSource.clip, Mathf.Clamp01(collision.impulse.magnitude.Remap(0, maxImpactForSound, 0, 1)));
        }
    }
}
