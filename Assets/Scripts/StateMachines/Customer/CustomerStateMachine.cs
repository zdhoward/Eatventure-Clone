using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerStateMachine : StateMachine
{
    [HideInInspector] public Cashierstation cashierstation;

    [HideInInspector] public Order order;

    [SerializeField] GameObject speechBubble;

    void Start()
    {
    }

    public void ShowSpeechBubble()
    {
        speechBubble.transform.eulerAngles = new Vector3(90, 0, 0);
        speechBubble.SetActive(true);
    }

    public void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }

    public void AssignCashierstation(Cashierstation cashierstation)
    {
        this.cashierstation = cashierstation;
        cashierstation.ReserveStation(this);

        SwitchState(new MoveState(this, cashierstation.CustomerTransform, new PlaceOrderState(this)));
    }

    public void AssignOrder(Order order)
    {
        this.order = order;
        speechBubble.transform.Find("Icon").GetComponent<Image>().sprite = order.orderItem.itemIcon;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
