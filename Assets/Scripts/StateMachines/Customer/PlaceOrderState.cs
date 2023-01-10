using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Customer State
public class PlaceOrderState : State
{
    CustomerStateMachine stateMachine;

    public PlaceOrderState(CustomerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Place Order State");
        stateMachine.cashierstation.PlaceOrder(stateMachine.order);
        Debug.Log("CUST: Placed Order at Cashierstation");

        stateMachine.SwitchState(new AwaitOrderState(stateMachine));
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
