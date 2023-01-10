using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Customer State
public class AwaitOrderState : State
{
    CustomerStateMachine stateMachine;

    public AwaitOrderState(CustomerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Await Order State");
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Await Order State");
        if (stateMachine.cashierstation.hasOrderBeenFulfilled)
        {
            stateMachine.cashierstation.PickupOrder();
            stateMachine.SwitchState(new MoveState(stateMachine, CustomerManager.Instance.SpawnPoint, new DespawnCustomerState(stateMachine)));
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Await Order State");
    }
}
