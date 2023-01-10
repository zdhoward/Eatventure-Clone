using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shared State
public class IdleState : State
{
    StateMachine stateMachine;

    public IdleState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Idle State");
        stateMachine.isIdle = true;
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Idle State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Idle State");
        stateMachine.isIdle = false;
    }
}
