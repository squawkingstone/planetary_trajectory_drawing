using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] double mass;
    [SerializeField] float speed;
    [SerializeField] double timestep;
    [SerializeField] int steps;

    [SerializeField] List<Rigidbody> planets;

    LineRenderer trajectory;

    double BigG = 6.674d * 1E-11d;

    void Awake()
    {
        trajectory = GetComponent<LineRenderer>();
    }

    [ContextMenu("Draw Trajectory")]
    void Draw()
    {
        // do this kind of iterative thing to do the arc sweeps
        List<Vector3> positions = new List<Vector3>();

        Vector3d position = new Vector3d(transform.position);
        Vector3d velocity = new Vector3d(transform.forward * speed);
        Vector3d acceleration = Vector3d.zero;

        positions.Add(position.ToVector3());

        Debug.ClearDeveloperConsole();
        for (int i = 0; i < steps; i++)
        {
            acceleration = ComputeNetGravity(position);
            Debug.Log(acceleration.magnitude);
            velocity += acceleration * timestep;
            position += velocity * timestep;
            positions.Add(position.ToVector3());
        }

        trajectory.positionCount = positions.Count;
        trajectory.SetPositions(positions.ToArray());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * speed);
        Gizmos.DrawSphere(transform.position, 0.25f);
    }

    Vector3d ComputeNetGravity(Vector3d pos)
    {
        Vector3d net = Vector3d.zero;
        for (int i = 0; i < planets.Count; i++)
        {
            net += ComputeGravityForce(pos, planets[i]);
        }
        return net;
    }

    Vector3d ComputeGravityForce(Vector3d pos, Rigidbody body)
    {
        Vector3d body_pos = new Vector3d(
            (double)body.transform.position.x,
            (double)body.transform.position.y,
            (double)body.transform.position.z
        );
        double a = (double)body.mass / (Vector3d.SqrDistance(pos, body_pos));
        return (body_pos - pos).normalized * a;
    }
}
