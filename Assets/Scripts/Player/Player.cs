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

    // UI
    public ArrowUI arrowUI;

    // Input
    Vector3 mouseDownPos;

    // Miscellaneous
    [Tooltip("Angle (in degrees) of the board on the x-axis")]
    public float BoardAngle = 45;



    public void AddForceImpulse(Vector3 direction, float magnitude) {
        rigidBody.AddForce(direction * magnitude, ForceMode.Impulse);
    }
    public void AddForce(Vector3 direction, float magnitude)
    {
        rigidBody.AddForce(direction * magnitude, ForceMode.Force);
    }
    public void ResetRigidbody()
    {
        rigidBody.velocity = Vector3.zero;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && IsOnPlatform())
        {
            Vector3 slingVector = -(Input.mousePosition - mouseDownPos);

            float magnitude = Vector3.Distance(Input.mousePosition, mouseDownPos);
            magnitude = magnitude.Remap(-100f, 100f, -maxMouseDragMagnitude, maxMouseDragMagnitude);
            magnitude = Mathf.Clamp(magnitude, -maxMouseDragMagnitude, maxMouseDragMagnitude);
            magnitude *= slingForce;

            SlingBall(slingVector.normalized, magnitude);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            arrowUI.ClearArrow();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 slingVector = -(Input.mousePosition - mouseDownPos);
            float arrowMaxLength = arrowUI.arrowMaxLength;

            float magnitude = Vector3.Distance(Input.mousePosition, mouseDownPos);
            magnitude = magnitude.Remap(-100f, 100f, -arrowMaxLength, arrowMaxLength);
            magnitude = Mathf.Abs(Mathf.Clamp(magnitude, -arrowMaxLength, arrowMaxLength));

            arrowUI.Draw(transform.position,
                         transform.position + slingVector.normalized * magnitude);
        }

        if (rigidBody.velocity.y < 0 && rigidBody.velocity.z < 0) {
            FallFaster();
        }
    }

    void FallFaster() {
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
        Vector3 forceDirection = Vector3.zero;

        // TODO: Calculate this better?
        forceDirection.x = direction.x;
        forceDirection.y = direction.y / 2f;
        forceDirection.z = direction.y / 2f;
        
        rigidBody.AddForce(forceDirection * magnitude, ForceMode.Impulse);
    }

    /// <summary>
    /// Checks to see if the player is on a platform
    /// </summary>
    /// <returns>True if a platform collision was found, false otherwise</returns>
    bool IsOnPlatform()
    {
        // TODO: Check if the player is above the platform, not just colliding with it
        int mask = 1 << 10; // Platforms on layer 10 in the inspector
        return Physics.CheckSphere(transform.position, transform.localScale.y / 2f + groundRaycastDistance, mask);
    }
}
