using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static Modifiers Instance;

    public static event Action OnModifierChanged;

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

    void IncreaseCostMultiplier(OrderItem orderItem)
    {
        // every 25 levels double the rank modifier

        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.costMultiplier *= 1.5f;
        OnModifierChanged?.Invoke();
    }

    public void DecreaseSpeedMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        upgrades.speedMultiplier *= 0.87f;
        OnModifierChanged?.Invoke();
    }

    public float GetOrderItemCostMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.costMultiplier;
    }

    public float GetOrderItemSpeedMultiplier(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.speedMultiplier;
    }

    public int GetUpgradeLevel(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return upgrades.upgradeLevel;
    }

    public Currency GetUpgradeCost(OrderItem orderItem)
    {
        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);
        return new Currency(upgrades.orderItem.itemBaseUpgradeCost * (upgrades.upgradeLevel + 1));
    }

    public bool TryUpgradeLevel(OrderItem orderItem)
    {
        if (!Wallet.Instance.TryRemoveGold(GetUpgradeCost(orderItem)))
            return false;

        WorkstationUpgrades upgrades = GetWorkstationUpgradesForOrderItem(orderItem);

        upgrades.upgradeLevel++;
        IncreaseCostMultiplier(upgrades.orderItem);

        OnModifierChanged?.Invoke();

        return true;
    }

    WorkstationUpgrades GetWorkstationUpgradesForOrderItem(OrderItem orderItem)
    {
        foreach (WorkstationUpgrades upgrades in workstationUpgrades)
        {
            if (upgrades.orderItem == orderItem)
            {
                return upgrades;
            }
        }
        return null;
    }
}

[Serializable]
public class WorkstationUpgrades
{
    public OrderItem orderItem;
    public float costMultiplier = 1f;
    public float speedMultiplier = 1f;
    public int upgradeLevel = 0;
}
