using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlanetRigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] double mass;
    [SerializeField] float speed;
    [SerializeField] double timestep;
    [SerializeField] int steps;

    PlanetAggregator aggregator;
    PlanetRigidbody planet;
    LineRenderer trajectory;

    void Awake()
    {
        aggregator = GameObject.Find("Planet Aggregator").GetComponent<PlanetAggregator>();
        planet = GetComponent<PlanetRigidbody>();
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

        for (int i = 0; i < steps; i++)
        {
            //acceleration = aggregator.ComputeNetGravity(position, planet);
            PlanetRigidbody.RK4Integrate(ref position, ref velocity, timestep, planet, aggregator);
            positions.Add(position.ToVector3());
        }

        trajectory.positionCount = positions.Count;
        trajectory.SetPositions(positions.ToArray());
    }

    [ContextMenu("Set Parameters")]
    void SetRigidbodyParameters()
    {
        planet.mass = mass;
        planet.velocity = (new Vector3d(transform.forward)) * speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * speed);
        Gizmos.DrawSphere(transform.position, 0.25f);
    }


}
