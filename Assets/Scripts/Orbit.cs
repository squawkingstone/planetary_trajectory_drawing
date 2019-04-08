using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConicSection
{
    Vector3 periapsis;
    Vector3 focus;
    float eccentricity;
    public Vector3[] Draw()
    {
        /* TODO this */
        return null;
    }
}

[RequireComponent(typeof(LineRenderer))]
public class Orbit : MonoBehaviour
{
    ConicSection orbit;

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        Vector3[] points = orbit.Draw();
        line.positionCount = points.Length;
        line.SetPositions(points);
    }
}
