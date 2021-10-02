using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public float bounceStrength;

    private void OnCollisionEnter(Collision other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player) {
            player.AddForceImpulse(transform.forward.normalized, bounceStrength);
        }
    }
}
