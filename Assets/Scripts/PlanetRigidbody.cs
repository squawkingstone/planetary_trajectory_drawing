using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO implement RK4 numerical integration
// this way I can just have the orbital dynamics live here and simulate forward to
// calculate the trajectory. I'm not gonna make this part of the actual dynamics
// unless I want to, but w/e we'll see

public class PlanetRigidbody : MonoBehaviour
{
    private static HashSet<PlanetRigidbody> bodies = null; // set of all bodies shared
    // between each thing, should also probably have mutexes cause stuff registering
    // at the same time might get bad

    public double mass;
    public Vector3d position { get; private set; }

    Vector3d velocity; // stored over time
    Vector3d acceleration; // accumulated based on other RBs

}
