using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherecastPerception : Perception
{
    public Transform raycastTransform;
    [Range(2, 50)] public int raycastNum = 2;
    [Range(1, 10)] public float radius = 1;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Vector3[] directions = Utilities.GetDirectionsInCircle(raycastNum, maxAngle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);
            Debug.DrawRay(ray.origin, ray.direction * distance);
            if (Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance))
            {
                if (raycastHit.collider.gameObject == gameObject)
                {
                    continue;
                }

                if (tagName == "" || raycastHit.collider.CompareTag(tagName))
                {
                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
                    result.Add(raycastHit.collider.gameObject);
                }
            }
        }

        result.Sort(CompareDistance);
        return result.ToArray();
    }
}
