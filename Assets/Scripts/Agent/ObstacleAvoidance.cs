using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public Transform raycastTransform;
    [Range(1, 40)] public float distance = 1;
    [Range(0, 180)] public float maxAngle = 45;
    [Range(2, 50)] public int numRaycast = 2;

    public LayerMask layerMask;

    public bool IsObstacleInFront()
    {
        // check if object is in front of agent 
        //Ray ray = new Ray(transform.position, transform.forward);
        //return Physics.Raycast(ray, distance, layerMask);
        Ray ray = new Ray(raycastTransform.position, raycastTransform.forward);
        return Physics.SphereCast(ray, 1, distance, layerMask);
    }

    public Vector3 GetOpenDirection()
    {
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);

        foreach(var direction in directions)
        {
            Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);
            if (!Physics.SphereCast(ray, 1, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);

                return ray.direction;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            }

        }
        return transform.forward;
    }
}
