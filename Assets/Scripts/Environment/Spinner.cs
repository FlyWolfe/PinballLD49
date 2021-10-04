using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float torqueForce;
    public float maxAngularVelocity;
    public Rigidbody rigidBody;
    public int score = 500;
    public float scoreThreshold = 200f;

    private void Start() {
        rigidBody.maxAngularVelocity = maxAngularVelocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Vector3 finalForce;

        if (player)
        {
            float additionalMagnitude = player.GetVelocityMagnitude();
            float direction = Mathf.Sign(player.transform.position.y - transform.position.y);
            Debug.LogWarning($"Force: {transform.up * -direction * torqueForce * additionalMagnitude}");
            finalForce = transform.up * -direction * torqueForce * additionalMagnitude;
            rigidBody.AddTorque(finalForce, ForceMode.Impulse);
            if (finalForce.magnitude > scoreThreshold)
                GameController.Instance.TargetScore += score;
        }
    }
}
