using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStateMachine : StateMachine
{
    [HideInInspector] public Workstation workstation;
    [HideInInspector] public Servingstation servingstation;

    public RadialTimer radialTimer;

    public Order order;

    void Start()
    {
        SwitchState(new IdleState(this));
    }

    public void CookOrder(Order order)
    {
        this.order = order;
        SwitchState(new MoveState(this, workstation.CookTransform, new CookOrderState(this)));
    }

    public void AssignWorkstation(Workstation workstation)
    {
        this.workstation = workstation;
    }

    public void AssignServingstation(Servingstation servingstation)
    {
        this.servingstation = servingstation;
    }
}
