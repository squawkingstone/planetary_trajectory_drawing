using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    So, one of my problems is error accumulation, my orbits are never stable as they get too out of
    hand too fast, what should happen is that I'm on a circular orbit, and if i increase my velocity
    then the orbit becomes more elliptical until it sort of "pops" into a parabolic curve at the limit
    velocity. But, what's happening is the way I'm doing the integration sort of makes stuff wobble off
    into hell no matter what, either slowly creep toward the sun, or slowly drift away. I'll just let 
    the simulation run for a while and see what happens, but my concern is that my integration
    scheme is introducing too much error, and that i'll have to do something more sophisticated
    like rk4 or Verlet. I'm also a bit concerned that my double size wont be big enough to deal with
    real planetary style numbers, but we'll cross that bridge when we get there.

    It might just be a matter of introducing a "drag" term in there to balance things out and keep the
    planet from flying off the handle, but we'll see

 */

public class PlanetRigidbody : MonoBehaviour
{
    public double mass;
    public Vector3d position;
    public bool stationary = true;

    public Vector3d velocity; // stored over time
    public Vector3d acceleration; // accumulated based on other RBs

    PlanetAggregator aggregator;

    void Awake()
    {
        aggregator = GameObject.Find("Planet Aggregator").GetComponent<PlanetAggregator>();
        position = new Vector3d(transform.position);
        Debug.Log(this);
    }

    void Update()
    {
        //Debug.Log(PlanetAggregator.bodies.Contains(this));
        if (!stationary)
        {
            RK4Integrate(ref position, ref velocity, Time.deltaTime, this, aggregator);
        }
        transform.position = position.ToVector3();
    }

    // Here we assume constant acceleration for the duration of the timestep
    public static void EulerIntegrate(ref Vector3d position, ref Vector3d velocity, Vector3d acceleration, double dt, 
        PlanetRigidbody self, PlanetAggregator aggr)
    {
        velocity += acceleration * dt;
        position += velocity * dt;
    }

    public static void MeanAccelerationIntegrate(ref Vector3d position, ref Vector3d velocity, Vector3d acceleration, double dt, 
        PlanetRigidbody self, PlanetAggregator aggr)
    {
        Vector3d expected_position = position + (velocity * dt);
        Vector3d expected_acceleration = aggr.ComputeNetGravity(expected_position, self);
        Vector3d new_velocity = velocity + ( 0.5 * (acceleration + expected_acceleration) * dt );
        position += 0.5 * (velocity + new_velocity) * dt;
        velocity = new_velocity;
    }

    public static void RK4Integrate(ref Vector3d position, ref Vector3d velocity, double dt, 
        PlanetRigidbody self, PlanetAggregator aggr)
    {
        Vector3d p1, p2, p3, p4;
        Vector3d v1, v2, v3, v4;
        Vector3d a1, a2, a3, a4;

        p1 = position;
        v1 = velocity;
        a1 = aggr.ComputeNetGravity(p1, self);

        p2 = position + 0.5*v1*dt;
        v2 = velocity + 0.5*a1*dt;
        a2 = aggr.ComputeNetGravity(p2, self);

        p3 = position + 0.5*v2*dt;
        v3 = velocity + 0.5*a2*dt;
        a3 = aggr.ComputeNetGravity(p3, self);

        p4 = position + v3*dt;
        v4 = velocity + a3*dt;
        a4 = aggr.ComputeNetGravity(p4, self);

        position += (dt / 6.0)*(v1 + 2.0*v2 + 2.0*v3 + v4);
        velocity += (dt / 6.0)*(a1 + 2.0*a2 + 2.0*a3 + a4);
    }

}
