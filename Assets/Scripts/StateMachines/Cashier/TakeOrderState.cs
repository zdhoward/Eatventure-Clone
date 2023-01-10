using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cashier State
public class TakeOrderState : State
{
    CashierStateMachine stateMachine;

    float takeOrderTimer;

    public TakeOrderState(CashierStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Take Order State");
        takeOrderTimer = GetTakeOrderTime();
        stateMachine.radialTimer.StartTimer(takeOrderTimer);
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Ticking Take Order State");
        takeOrderTimer = Mathf.Max(0f, takeOrderTimer -= deltaTime);

        if (takeOrderTimer == 0f)
        {
            // Submit order to CookManager
            Debug.Log("CASH: Order Taken at Cashierstation");
            Order order = stateMachine.cashierstation.TakeOrder();
            CookManager.Instance.ReceiveOrder(order);
            stateMachine.SwitchState(new IdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Take Order State");
    }

    float GetTakeOrderTime()
    {
        return 5f;
    }
}
