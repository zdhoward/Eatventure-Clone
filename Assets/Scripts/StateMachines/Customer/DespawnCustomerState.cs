using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCustomerState : State
{
    CustomerStateMachine stateMachine;

    public DespawnCustomerState(CustomerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Place Order State");
        Debug.Log("CUST: Left with order");

        CustomerManager.Instance.DespawnCustomer(stateMachine);
        stateMachine.Despawn();
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Place Order State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Place Order State");
    }
}
