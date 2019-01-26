using System;
using UnityEngine;

[System.Serializable]
public class Vector3d
{
    public double x;
    public double y;
    public double z;

    /* ------------------------------------------------------------ */
    /* STATIC PROPERTIES */
    /* ------------------------------------------------------------ */
    public static Vector3d zero
    {
        get
        {
            return new Vector3d(0d, 0d, 0d);
        }
    }

    /* ------------------------------------------------------------ */
    /* INSTANCE PROPERTIES */
    /* ------------------------------------------------------------ */

    public double magnitude
    {
        get
        {
            return Vector3d.SqrMagnitude(this);
        }
    }

    public double sqrMagnitude
    {
        get
        {
            return Vector3d.Magnitude(this);
        }
    }

    public Vector3d normalized
    {
        get
        {
            return Vector3d.Normalize(this);
        }
    }

    /* ------------------------------------------------------------ */
    /* CONSTRUCTORS */
    /* ------------------------------------------------------------ */

    public Vector3d(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3d(Vector3 v)
    {
        x = (double)v.x;
        y = (double)v.y;
        z = (double)v.z;
    }

    /* ------------------------------------------------------------ */
    /* OPERATORS */
    /* ------------------------------------------------------------ */

    public static Vector3d operator +(Vector3d v, Vector3d u)
    {
        return new Vector3d(v.x + u.x, v.y + u.y, v.z + u.z);
    }

    public static Vector3d operator -(Vector3d v, Vector3d u)
    {
        return new Vector3d(v.x - u.x, v.y - u.y, v.z - u.z);
    }

    public static Vector3d operator *(Vector3d v, double s)
    {
        return new Vector3d(v.x * s, v.y * s, v.z * s);
    }

    public static Vector3d operator *(double s, Vector3d v)
    {
        return new Vector3d(v.x * s, v.y * s, v.z * s);
    }

    public static Vector3d operator /(Vector3d v, double s)
    {
        return new Vector3d(v.x / s, v.y / s, v.z / s);
    }

    /* ------------------------------------------------------------ */
    /* PUBLIC STATIC FUNCTIONS */
    /* ------------------------------------------------------------ */

    public static double SqrDistance(Vector3d v, Vector3d u)
    {
        double dx = v.x - u.x;
        double dy = v.y - u.y;
        double dz = v.z - u.z;
        return (dx * dx) + (dy * dy) + (dz * dz);
    }

    /* ------------------------------------------------------------ */
    /* PRIVATE STATIC FUNCTIONS */
    /* ------------------------------------------------------------ */

    private static double SqrMagnitude(Vector3d v)
    {
        return (v.x * v.x) + (v.y * v.y) + (v.z * v.z);
    }

    private static double Magnitude(Vector3d v)
    {
        return Math.Sqrt(Vector3d.SqrMagnitude(v));
    }

    private static Vector3d Normalize(Vector3d v)
    {
        double mag = v.magnitude;
        if (mag > 9.99999974737875E-06)
        {
            return v / mag;
        }
        else
        {
            return Vector3d.zero;
        }
    }

    /* ------------------------------------------------------------ */
    /* CONVERSIONS */
    /* ------------------------------------------------------------ */

    public Vector3 ToVector3()
    {
        return new Vector3((float)x, (float)y, (float)z);
    }
}
