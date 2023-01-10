using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashierstation : MonoBehaviour
{
    float gridSize = 10f;

    [field: SerializeField] public Transform CustomerTransform { get; private set; }
    [field: SerializeField] public Transform CashierTransform { get; private set; }

    public bool isReservedByCustomer { get; private set; }
    public bool isReservedByCashier { get; private set; }
    public bool hasCustomer { get; private set; }
    public bool hasOrder { get; private set; }
    public bool hasOrderBeenFulfilled { get; private set; }

    public Order order { get; private set; }

    public void ReserveStation(CustomerStateMachine customer)
    {
        isReservedByCustomer = true;
    }

    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }

    public void PlaceOrder(Order order)
    {
        hasCustomer = true;
        this.order = order;
    }

    public Order TakeOrder()
    {
        hasOrder = true;
        order.customer.ShowSpeechBubble();
        return order;
    }

    public void FulfillOrder()
    {
        hasOrderBeenFulfilled = true;
        order.customer.HideSpeechBubble();
    }

    public void PickupOrder()
    {
        isReservedByCustomer = false;
        isReservedByCashier = false;
        hasCustomer = false;
        hasOrder = false;
        hasOrderBeenFulfilled = false;
    }

    void OnDrawGizmos()
    {
        // Cashier position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + -transform.forward * gridSize, Vector3.one * gridSize);

        // Customer position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward * gridSize, Vector3.one * gridSize);
    }
}
