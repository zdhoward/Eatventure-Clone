using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierStateMachine : StateMachine
{
    [HideInInspector] public Cashierstation cashierstation;
    [HideInInspector] public Servingstation servingstation;

    public RadialTimer radialTimer;

    public Order order;

    void Start()
    {
        SwitchState(new IdleState(this));
    }

    public void TakeOrder(Cashierstation cashierstation)
    {
        this.cashierstation = cashierstation;
        cashierstation.ReserveStation(this);
        SwitchState(new MoveState(this, cashierstation.CashierTransform, new TakeOrderState(this)));
    }

    public void RetreiveOrder(Servingstation servingstation)
    {
        this.servingstation = servingstation;
        servingstation.ReserveStation(this);
        SwitchState(new MoveState(this, servingstation.CashierTransform, new RetrieveOrderState(this)));
    }

    public void FulfillOrder(Cashierstation cashierstation)
    {
        this.cashierstation = cashierstation;
        SwitchState(new MoveState(this, cashierstation.CashierTransform, new FulfillOrderState(this)));
    }
}
