using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Servingstation : MonoBehaviour
{
    float gridSize = 10f;

    [field: SerializeField] public Transform CookTransform { get; private set; }
    [field: SerializeField] public Transform CashierTransform { get; private set; }

    [SerializeField] GameObject orderDisplay;
    [SerializeField] Image orderDisplayIcon;
    [SerializeField] TextMeshProUGUI orderDisplayValueLabel;

    // Can have many orders on top of eachother
    public bool hasOrder { get; private set; }

    List<Order> orders = new List<Order>();

    public bool isReservedByCashier { get; private set; }

    public void ServeOrder(Order order)
    {
        hasOrder = true;
        orders.Add(order);

        ShowOrderDisplay();
    }

    public Order PickupOrder()
    {
        hasOrder = false;
        isReservedByCashier = false;
        Order order = orders[0];
        orders.RemoveAt(0);

        if (orders.Count < 1)
            HideOrderDisplay();

        return order;
    }

    public void ReserveStation(CashierStateMachine cashier)
    {
        isReservedByCashier = true;
    }

    void ShowOrderDisplay()
    {
        UpdateOrderDisplay();
        orderDisplay.SetActive(true);
    }

    void HideOrderDisplay()
    {
        orderDisplay.SetActive(false);
    }

    void UpdateOrderDisplay()
    {
        if (orders.Count > 0)
        {
            orderDisplayIcon.sprite = orders[0].orderItem.itemIcon;
            orderDisplayValueLabel.text = "<sprite name=\"CoinIcon\">" + new Currency(orders[0].orderItem.GetCost()).ToShortString();
        }
        else
            orderDisplayIcon.sprite = null;
    }

    void OnDrawGizmos()
    {
        // Cook position
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + -transform.forward * gridSize, Vector3.one * gridSize);

        // Cashier position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + transform.forward * gridSize, Vector3.one * gridSize);
    }
}
