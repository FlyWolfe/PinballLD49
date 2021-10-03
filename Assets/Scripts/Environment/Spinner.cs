using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float torqueForce;
    public Rigidbody rigidBody;


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player)
        {
            float direction = player.transform.position.y - transform.position.y;
            rigidBody.AddTorque(transform.up * -direction * torqueForce, ForceMode.Impulse);
        }
    }
}
