using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cashier State
public class FulfillOrderState : State
{
    CashierStateMachine stateMachine;

    public FulfillOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Fulfill Order State");
        Wallet.Instance.AddGold(stateMachine.order.orderItem.GetCost());

        stateMachine.cashierstation.FulfillOrder();
        Debug.Log("CASH: Delivered Order to Cashierstation");

        stateMachine.SwitchState(new IdleState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Fulfill Order State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Fulfill Order State");
    }
}
