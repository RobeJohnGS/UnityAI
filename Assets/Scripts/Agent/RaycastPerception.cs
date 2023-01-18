using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPerception : Perception
{
    public Transform raycastTransform;
    [Range(2, 50)] public int raycastNum = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Vector3[] directions = Utilities.GetDirectionsInCircle(raycastNum, maxAngle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, distance))
            {
                if (raycastHit.collider.gameObject == gameObject)
                {
                    continue;
                }

                if (tagName == "" || raycastHit.collider.CompareTag(tagName))
                {
                    result.Add(raycastHit.collider.gameObject);
                }
            }
        }

        result.Sort(CompareDistance);
        return result.ToArray();
    }
}
