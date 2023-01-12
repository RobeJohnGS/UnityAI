using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        return v;
    }

    public static Vector3 ClampMagnitude(Vector3 v, float min, float max)
    {
        return v.normalized * Mathf.Clamp(v.magnitude, min, max);
    }
}
