using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));

        return force;
    }

    public static Vector3 Flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));

        return force;
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.data.wanderDisplacement, agent.data.wanderDisplacement);
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * agent.data.wanderRadius);
        Vector3 forward = agent.transform.forward * agent.data.wanderDistance;

        Vector3 force = CalculateSteering(agent, forward + point);

        return force;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 ndirection = direction.normalized;
        Vector3 desired = ndirection * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }

    public static Vector3 Cohesion(Agent agent, GameObject[] neighbors)
    {
        Vector3 center = Vector3.zero;

        foreach(var neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }

        center = center / neighbors.Length;

        Vector3 force = CalculateSteering(agent, center - agent.transform.position);

        return force;
    }

    public static Vector3 Seperation(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 sepearation = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            Vector3 direction = agent.transform.position - neighbor.transform.position;
            if (direction.magnitude < radius)
            {
                sepearation += direction / direction.sqrMagnitude;
            }
        }
        Vector3 force = CalculateSteering(agent, sepearation);

        return force;
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 averageVelocity = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            averageVelocity += neighbor.GetComponent<Agent>().movement.velocity;
        }
        averageVelocity /= neighbors.Length;

        Vector3 force = CalculateSteering(agent, averageVelocity);

        return force;
    }
}
