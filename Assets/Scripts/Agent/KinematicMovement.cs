using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMovement : Movement
{
    //[Range(1, 10)] public float maxSpeed = 5;
    //[Range(1, 10)] public float minSpeed = 1;
    //[Range(1, 100)] public float maxForce = 5;

    //public Vector3 velocity { get; set; } = Vector3.zero;
    //public Vector3 acceleration { get; set; } = Vector3.zero;
    //public Vector3 dircection { get { return velocity.normalized; } }

    public override void ApplyForce(Vector3 force)
    {
        acceleration += force;
    }

    public override void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        ApplyForce(direction * maxForce);
    }

    public override void Stop()
    {
        velocity = Vector3.zero;
    }

    public override void Resume()
    {
        //When you are at raymond maple class
        //yeah Im talking to you github gremlins
    }

    void LateUpdate()
    {
        velocity += acceleration * Time.deltaTime;
        velocity = Utilities.ClampMagnitude(velocity, minSpeed, maxSpeed);

        transform.position += velocity * Time.deltaTime;
        if (velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        acceleration = Vector3.zero;
    }
}