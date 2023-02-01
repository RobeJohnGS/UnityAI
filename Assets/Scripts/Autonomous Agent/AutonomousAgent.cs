using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] private Perception flockPerception;
    public ObstacleAvoidance obstacleAvoidance;
    public AutonomousAgentData data;
    
    
    //public float wanderDistance = 1;
    //public float wanderRadius = 3;
    //public float wanderDisplacement = 5;

    public float wanderAngle { get; set; } = 0;

    // Update is called once per frame
    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (gameObjects.Length > 0)
        {
            //Vector3 direction = (gameObjects[0].transform.position - transform.position).normalized;
            //Debug.DrawRay(transform.position, direction);
            //movement.ApplyForce(direction * 4);

            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.seekWeight);
        }

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length > 0)
        {
            //foreach (var gameObject in gameObjects)
            //{
            //    movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            //    movement.ApplyForce(Steering.Seperation(this, gameObjects, data.separationRadius) * data.separationWeight);
            //    movement.ApplyForce(Steering.Alignment(this, gameObjects, data.separationRadius) * data.alignmentWeight);
            //}
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Seperation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, gameObjects, data.separationRadius) * data.alignmentWeight);
        }
        

        if (obstacleAvoidance.IsObstacleInFront())
        {
            Vector3 direction = obstacleAvoidance.GetOpenDirection();
            movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
