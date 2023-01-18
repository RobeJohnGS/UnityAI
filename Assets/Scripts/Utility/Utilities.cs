using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;
        if (result.x > max.x)
        {
            result.x = min.x;
        }else if (result.x < min.x)
        {
            result.x = max.x;
        }

        if (result.y > max.y)
        {
            result.y = min.y;
        }
        else if (result.x < min.x)
        {
            result.y = max.y;
        }

        if (result.z > max.z)
        {
            result.z = min.z;
        }
        else if (result.z < min.z)
        {
            result.z = max.z;
        }

        return result;
    }

    public static Vector3 ClampMagnitude(Vector3 v, float min, float max)
    {
        return v.normalized * Mathf.Clamp(v.magnitude, min, max);
    }

    public static Vector3[] GetDirectionsInCircle(int num, float angle)
    {
        List<Vector3> result = new List<Vector3>();

        if (num % 2 == 1)
        {
            result.Add(Vector3.forward);
        }

        float angleOffSet = angle / (num - 1);

        for (int i = 0; i < num / 2; i++)
        {
            result.Add(Quaternion.AngleAxis(+angleOffSet * i, Vector3.up) * Vector3.forward);
            result.Add(Quaternion.AngleAxis(-angleOffSet * i, Vector3.up) * Vector3.forward);
        }

        return result.ToArray();
    }
}
