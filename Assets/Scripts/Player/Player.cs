using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // TODO: Rename these to make more sense
    // Physics
    public float slingForce = 20f;
    [Tooltip("The maximum force increment from dragging the mouse")]
    public float maxMouseDragMagnitude = 10f;
    [Tooltip("How far the user should drag in screen space to get the maximum drag power")]
    public float mouseDragScale = 100f;
    public float fallGravityAddition = 1f;
    Rigidbody rigidBody;

    // Collision
    [Tooltip("Distance from the center of the ball to cast a ray for checking if on a platform")]
    public float groundRaycastDistance = 5f;
    public float maxImpactForSound = 50f;

    //SFX
    //public float rollingSoundMagnitudeThreshold = 0.1f;
    //public AudioSource rollingSound;
    public AudioSource hitSound;

    // UI
    public ArrowUI arrowUI;

    // Input
    Vector3 mouseDownPos;
    public float curvePower = 4f;

    // Miscellaneous
    [Tooltip("The game board (used for vector angle calculation)")]
    public Transform board;



    public void AddForceImpulse(Vector3 direction, float magnitude)
    {
        rigidBody.AddForce(direction * magnitude, ForceMode.Impulse);
    }
    public void AddForce(Vector3 direction, float magnitude)
    {
        rigidBody.AddForce(direction * magnitude, ForceMode.Force);
    }
    public void ResetRigidbody()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
    public float GetVelocityMagnitude()
    {
        return rigidBody.velocity.magnitude;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameController.Instance.IsGameRunning == false)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && IsOnPlatform())
        {
            Vector3 slingVector = -(Input.mousePosition - mouseDownPos);

            float magnitude = Vector3.Distance(Input.mousePosition, mouseDownPos);
            magnitude = magnitude.Remap(-mouseDragScale, mouseDragScale, -maxMouseDragMagnitude, maxMouseDragMagnitude);
            magnitude = Mathf.Clamp(magnitude, -maxMouseDragMagnitude, maxMouseDragMagnitude);

            float powerValue = magnitude.Remap(-maxMouseDragMagnitude, maxMouseDragMagnitude, -1f, 1f);
            powerValue = Mathf.Abs(magnitude);
            magnitude *= Mathf.Pow(powerValue, curvePower);

            magnitude *= slingForce;

            SlingBall(slingVector.normalized, magnitude);
        }

        if (Input.GetMouseButton(0) && IsOnPlatform())
        {
            Vector3 slingVector = -(Input.mousePosition - mouseDownPos);
            float arrowMaxLength = arrowUI.arrowMaxLength;

            float magnitude = Vector3.Distance(Input.mousePosition, mouseDownPos);
            magnitude = magnitude.Remap(-mouseDragScale, mouseDragScale, -arrowMaxLength, arrowMaxLength);
            magnitude = Mathf.Abs(Mathf.Clamp(magnitude, -arrowMaxLength, arrowMaxLength));

            arrowUI.Draw(transform.position,
                         transform.position + slingVector.normalized * magnitude);
        }
        else
        {
            arrowUI.ClearArrow();
        }

        if (rigidBody.velocity.y < 0 && rigidBody.velocity.z < 0)
        {
            FallFaster();
        }

        /*if(rollingSound.isPlaying == false && rigidBody.velocity.magnitude > rollingSoundMagnitudeThreshold) 
        {
            rollingSound.Play();
        }
        else if(rollingSound.isPlaying && rigidBody.velocity.magnitude < rollingSoundMagnitudeThreshold)
        {
            rollingSound.Stop();
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 13) //= board
            if (hitSound.isPlaying == false) {
                hitSound.PlayOneShot(hitSound.clip, Mathf.Clamp01(collision.impulse.magnitude.Remap(0, maxImpactForSound, 0, 1)));
            }

    }

    void FallFaster()
    {
        Vector3 forceDirection = rigidBody.velocity;
        forceDirection.x = 0f;
        AddForce(forceDirection.normalized, fallGravityAddition);
    }

    /// <summary>
    /// Applies an impulse force to the player in the given direction and with the given magnitude
    /// </summary>
    /// <param name="slingDirection">The directional vector to sling the player</param>
    void SlingBall(Vector2 direction, float magnitude)
    {
        Vector3 forceDirection = Quaternion.Euler(board.eulerAngles.x, 0, 0) * (Vector3)direction;

        rigidBody.AddForce(forceDirection * magnitude, ForceMode.Impulse);
    }

    /// <summary>
    /// Checks to see if the player is on a platform
    /// </summary>
    /// <returns>True if a platform collision was found, false otherwise</returns>
    bool IsOnPlatform()
    {
        int mask = 1 << 10; // Platforms on layer 10 in the inspector
        return Physics.CheckSphere(transform.position + ((transform.localScale.y / 2f) * (-board.up)) , transform.localScale.y / 4f + groundRaycastDistance, mask);
    }
}
