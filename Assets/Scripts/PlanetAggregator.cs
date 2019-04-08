using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAggregator : MonoBehaviour
{
    public List<PlanetRigidbody> bodies;

    double BigG = 6.674d * 1E-11d; // gravitational constant, who knows if I'll use it

    void Awake()
    {
        bodies = new List<PlanetRigidbody>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Planet"))
        {
            bodies.Add(g.GetComponent<PlanetRigidbody>());
        }
    }

    public Vector3d ComputeNetGravity(Vector3d position, PlanetRigidbody self)
    {
        Vector3d net = Vector3d.zero;
        foreach (PlanetRigidbody body in bodies)
        {
            if (body != self)
            {
                net += ComputeGravityForce(position, body);
            }
        }
        return net;
    }
    
    public Vector3d ComputeNetGravity(Vector3d position)
    {
        Vector3d net = Vector3d.zero;
        foreach (PlanetRigidbody body in bodies)
        {
            net += ComputeGravityForce(position, body);
        }
        return net;
    }

    public Vector3d ComputeGravityForce(Vector3d position, PlanetRigidbody body)
    {
        Vector3d body_pos = new Vector3d(
            (double)body.transform.position.x,
            (double)body.transform.position.y,
            (double)body.transform.position.z
        );

        double r = Vector3d.SqrDistance(position, body_pos);

        // I'm fine with this approximation because it solves the problem of the div by zero of
        // objects trying to attract themselves, and two objects centers overlapping is pretty nonsense
        // conceptually
        if (r <= Vector3d.MIN_DOUBLE) return Vector3d.zero;
        
        double a = (double)body.mass / r;
        return (body_pos - position).normalized * a;
    }
}
