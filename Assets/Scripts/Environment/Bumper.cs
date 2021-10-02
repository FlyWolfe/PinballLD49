using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounceStrength;

    private void OnCollisionEnter(Collision other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player) {
            Vector3 direction = player.transform.position - transform.position;
            player.AddForceImpulse(direction.normalized, bounceStrength);
        }
    }
}
