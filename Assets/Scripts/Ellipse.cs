using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Ellipse : MonoBehaviour
{
    [Range(3, 36)]
    public int steps = 3;
    public float xAxis;
    public float yAxis;

    LineRenderer orbit;

    void Awake()
    {
        orbit = GetComponent<LineRenderer>();
    }

    void Start()
    {
        DrawEllipse();
    }

    [ContextMenu("Draw Ellipse")]
    void DrawEllipse()
    {
        Vector3[] positions = new Vector3[steps];
        for (int i = 0; i < steps; i++)
        {
            float angle = ((float)i / (float)steps) * 2.0f * Mathf.PI;
            float x = Mathf.Sin(angle) * xAxis;
            float y = Mathf.Cos(angle) * yAxis;
            positions[i] = new Vector3(x, 0f, y);
        }

        orbit.positionCount = steps;
        orbit.SetPositions(positions);
    }

    void OnValidate() { if (Application.isPlaying) DrawEllipse(); }
}
