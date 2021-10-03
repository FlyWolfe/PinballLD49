using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float torqueForce;
    public float maxAngularVelocity;
    public Rigidbody rigidBody;

    private void Start() {
        rigidBody.maxAngularVelocity = maxAngularVelocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        float additionalMagnitude = player.GetVelocityMagnitude();

        if (player)
        {
            float direction = Mathf.Sign(player.transform.position.y - transform.position.y);
            Debug.LogWarning($"Force: {transform.up * -direction * torqueForce * additionalMagnitude}");
            rigidBody.AddTorque(transform.up * -direction * torqueForce * additionalMagnitude, ForceMode.Impulse);
        }
    }
}
