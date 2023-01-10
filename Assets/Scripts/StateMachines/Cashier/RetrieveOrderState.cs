using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cashier State
public class RetrieveOrderState : State
{
    CashierStateMachine stateMachine;

    public RetrieveOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Retreive Order State");
        stateMachine.order = stateMachine.servingstation.PickupOrder();

        stateMachine.cashierstation = CashierManager.Instance.FindCashierstationOfOrder(stateMachine.order);

        Debug.Log("CASH: Picked Up Order at Servingstation");

        stateMachine.SwitchState(new MoveState(stateMachine, stateMachine.cashierstation.CashierTransform, new FulfillOrderState(stateMachine)));
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Retreive Order State");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Retreive Order State");
    }
}
