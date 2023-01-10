using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new OrderItem", menuName = "ScriptableObjects/OrderItem", order = 1)]
public class OrderItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public double itemBaseCost;
    public float itemBaseCookTime;

    public double GetCost()
    {
        double itemCost = RoundDouble(itemBaseCost * Modifiers.Instance.GetOrderItemCostMultiplier(this));
        return itemCost;
    }

    public float GetCookTime()
    {
        float itemCookTime = itemBaseCookTime * Modifiers.Instance.GetOrderItemSpeedMultiplier(this);
        return itemCookTime;
    }

    double RoundDouble(double number)
    {
        double mod = number % 1;
        return number - mod;
    }
}
