using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrowUI : MonoBehaviour
{
    [Tooltip("The percent of the line that is consumed by the arrowhead")]
    [Range(0, 1)]
    public float percentHead = 0.4f;
    public float arrowMaxLength = 5f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ClearArrow();
    }

    public void Draw(Vector3 arrowStart, Vector3 arrowEnd)
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;
            
        lineRenderer.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.4f), 
            new Keyframe(0.999f - percentHead, 0.4f),   // neck of arrow
            new Keyframe(1 - percentHead, 1f),  // max width of arrow head
            new Keyframe(1, 0f)
        );  // tip of arrow
        
        lineRenderer.SetPositions(new Vector3[] {
            arrowStart,
            Vector3.Lerp(arrowStart, arrowEnd, 0.999f - percentHead),
            Vector3.Lerp(arrowStart, arrowEnd, 1 - percentHead),
        });
    }

    public void ClearArrow() {
        lineRenderer.enabled = false;
    }
}
