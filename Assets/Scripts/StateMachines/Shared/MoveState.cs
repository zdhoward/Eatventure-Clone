using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shared State
public class MoveState : State
{
    StateMachine stateMachine;

    Transform destination;

    State stateToChangeTo;

    public MoveState(StateMachine stateMachine, Transform destination, State stateToChangeTo)
    {
        this.stateMachine = stateMachine;
        this.destination = destination;
        this.stateToChangeTo = stateToChangeTo;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Move State");
        stateMachine.agent.enabled = true;
        stateMachine.agent.SetDestination(destination.position);
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Move State");

        if (stateMachine.agent.pathPending)
            return;

        if (stateMachine.agent.remainingDistance < 0.1f)
        {
            stateMachine.transform.SetPositionAndRotation(destination.position, destination.rotation);
            stateMachine.SwitchState(stateToChangeTo);
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Move State");
        stateMachine.agent.enabled = false;
    }
}
