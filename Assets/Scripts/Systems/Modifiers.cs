using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static Modifiers Instance;

    [field: SerializeField] public int maxCustomers { get; private set; } = 1;
    [field: SerializeField] public int maxCashiers { get; private set; } = 1;
    [field: SerializeField] public int maxCooks { get; private set; } = 1;

    [field: SerializeField] public List<WorkstationUpgrades> workstationUpgrades { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of Modifiers in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void IncreaseMaxCustomers(int amount = 1)
    {
        maxCustomers += amount;
    }
    public void IncreaseMaxCashiers(int amount = 1)
    {
        maxCashiers += amount;
    }
    public void IncreaseMaxCooks(int amount = 1)
    {
        maxCooks += amount;
    }

    public void IncreaseCostMultiplier(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {
            Debug.Log("Comparing: " + upgrades.orderItem.itemName + " with " + orderItem.itemName);
            if (upgrades.orderItem == orderItem)
            {
                upgrades.costMultiplier *= 1.5f;
                return;
            }
        }
    }

    public void DecreaseSpeedMultiplier(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {

            if (upgrades.orderItem == orderItem)
            {
                upgrades.speedMultiplier *= 0.87f;
                return;
            }
        }
    }

    public float GetOrderItemCostMultiplier(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {
            if (upgrades.orderItem == orderItem)
            {
                return upgrades.costMultiplier;
            }
        }

        return 1f;
    }

    public float GetOrderItemSpeedMultiplier(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {
            if (upgrades.orderItem == orderItem)
            {
                return upgrades.speedMultiplier;
            }
        }

        return 1f;
    }
}

[Serializable]
public class WorkstationUpgrades
{
    public OrderItem orderItem;
    public float costMultiplier = 1f;
    public float speedMultiplier = 1f;
}
