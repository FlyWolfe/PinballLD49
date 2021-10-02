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
    Rigidbody rigidBody;

    // Collision
    [Tooltip("Distance from the center of the ball to cast a ray for checking if on a platform")]
    public float groundRaycastDistance = 5f;

    // Input
    Vector3 mouseDownPos;

    // Miscellaneous
    [Tooltip("Angle (in degrees) of the board on the x-axis")]
    public float BoardAngle = 45;



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
        if (Input.GetMouseButtonUp(0) && IsOnPlatform())
        {
            Vector3 slingVector = -(Input.mousePosition - mouseDownPos);

            float magnitude = Vector3.Distance(Input.mousePosition, mouseDownPos);
            magnitude = magnitude.Remap(-100f, 100f, -maxMouseDragMagnitude, maxMouseDragMagnitude);
            magnitude = Mathf.Clamp(magnitude, -maxMouseDragMagnitude, maxMouseDragMagnitude);
            magnitude *= slingForce;

            SlingBall(slingVector.normalized, magnitude);
        }
    }

    /// <summary>
    /// Applies an impulse force to the player in the given direction and with the given magnitude
    /// </summary>
    /// <param name="slingDirection">The directional vector to sling the player</param>
    void SlingBall(Vector2 slingDirection, float magnitude)
    {
        Vector3 forceDirection = Vector3.zero;

        // TODO: Calculate this better?
        forceDirection.x = slingDirection.x;
        forceDirection.y = slingDirection.y / 2f;
        forceDirection.z = slingDirection.y / 2f;
        
        rigidBody.AddForce(forceDirection * magnitude, ForceMode.Impulse);
    }

    bool IsOnPlatform() {
        int mask = 1 << 10;    // Ground on layer 10 in the inspector
        return Physics.CheckSphere(transform.position, transform.localScale.y / 2f + groundRaycastDistance, mask);
    }
}
