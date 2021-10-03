using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippers : MonoBehaviour
{
    public Transform rotationRoot;
    public float rotation;
    public float rotationTime;
    public float rotationDelay;
    public float holdDelay = 0.1f;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Rigidbody rigidBody;


    private void Start()
    {
        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(Vector3.forward * rotation);
        rigidBody = rotationRoot.GetComponent<Rigidbody>();

        /*Debug.Log(startRotation.eulerAngles);
        Debug.Log(endRotation.eulerAngles);
        SetRootRotation(endRotation);*/
        StartCoroutine(FlipperRoutine());
    }

    private IEnumerator FlipperRoutine()
    {
        while (true)
        {
            yield return Lerp(startRotation, endRotation, rotationTime, SetRootRotation);
            yield return new WaitForSeconds(holdDelay);
            yield return Lerp(endRotation, startRotation, rotationTime, SetRootRotation);
            yield return new WaitForSeconds(rotationDelay);
        }
    }

    private void SetRootRotation(Quaternion quaternion)
    {
        rigidBody.MoveRotation(quaternion);
        // OLD // rotationRoot.rotation = quaternion;
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
