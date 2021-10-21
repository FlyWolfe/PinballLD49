using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippers : MonoBehaviour
{
    /// <summary>
    /// The Transform of the object whose center is the center of rotation for the flipper
    /// </summary>
    public Transform rotationRoot;
    /// <summary>
    /// The amount of rotation, in degrees, that the flipper should move from its starting rotateion
    /// </summary>
    public float rotation;
    /// <summary>
    /// The amount of time, in seconds, that the rotation should take
    /// </summary>
    public float rotationTime;
    
    // TODO: Add summaries to the following
    public float rotationDelay;
    public float holdDelay = 0.1f;
    public float rotationDelayOffset = 0.5f;

    /// <summary>
    /// The AudioSource to play clips when the flipper is moving up
    /// </summary>
    public AudioSource audioSourceUp;
    /// <summary>
    /// The AudioSource to play clips when the flipper is moving down
    /// </summary>
    public AudioSource audioSourceDown;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Rigidbody rigidBody;


    private void Start()
    {
        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(Vector3.forward * rotation);
        rigidBody = rotationRoot.GetComponent<Rigidbody>();

        StartCoroutine(FlipperRoutine());
    }

    /// <summary>
    /// Rotates the flippers 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlipperRoutine()
    {
        yield return new WaitForSeconds(rotationDelay + UnityEngine.Random.Range(-rotationDelayOffset, rotationDelayOffset));
        while (true)
        {
            audioSourceUp.Play();
            yield return Lerp(startRotation, endRotation, rotationTime, SetRootRotation);
            yield return new WaitForSeconds(holdDelay);
            audioSourceDown.Play();
            yield return Lerp(endRotation, startRotation, rotationTime, SetRootRotation);
            yield return new WaitForSeconds(rotationDelay + UnityEngine.Random.Range(-rotationDelayOffset, rotationDelayOffset));
        }
    }

    /// <summary>
    /// Rotates the flipper via its RigidBody so physics objects interact with it
    /// </summary>
    /// <param name="quaternion">The rotational quaternion to adjust the RigidBody with</param>
    private void SetRootRotation(Quaternion quaternion)
    {
        rigidBody.MoveRotation(quaternion);
    }

    private IEnumerator Lerp(Quaternion start, Quaternion end, float time, Action<Quaternion> onStep)
    {
        float duration = 0f;
        onStep.Invoke(start);
        while (duration < time)
        {
            //Debug.Log($"{start.eulerAngles} > {end.eulerAngles} : { Mathf.Clamp01(duration)}");
            onStep.Invoke(Quaternion.Lerp(start, end, Mathf.Clamp01(duration/time)));
            yield return new WaitForEndOfFrame();
            duration += Time.deltaTime;
        }
        onStep.Invoke(end);
    }
}
