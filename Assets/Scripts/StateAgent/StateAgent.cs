using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    public StateMachine stateMachine = new StateMachine();
    public GameObject[] percieved;

    void Start()
    {
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new PatrolState(this));
        stateMachine.AddState(new ChaseState(this));
        stateMachine.StartState(nameof(IdleState));
        //stateMachine.StartState(nameof(PatrolState));
    }

    void Update()
    {
        percieved = perception.GetGameObjects();

        stateMachine.Update();

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }
}
