using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;

    public static event Action<Currency> OnGoldValueChanged;


    Currency goldAmount = new Currency(0);

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of Wallet in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        OnGoldValueChanged?.Invoke(goldAmount);
    }

    public void AddGold(double amtToAdd)
    {
        goldAmount += amtToAdd;

        OnGoldValueChanged?.Invoke(goldAmount);

        //Debug.Log($"Added {amtToAdd} gold to the Wallet. Current Balance: {goldAmount}");
    }

    public bool TryRemoveGold(double amtToRemove)
    {
        if (goldAmount < amtToRemove)
            return false;

        goldAmount -= amtToRemove;

        OnGoldValueChanged?.Invoke(goldAmount);

        //Debug.Log($"Removed {amtToRemove} gold from the Wallet. Current Balance: {goldAmount}");

        return true;
    }

    public Currency GetGoldBalance()
    {
        return goldAmount;
    }

    // For Debugging
    public void SetGold(double amt)
    {
        goldAmount = new Currency(amt);

        OnGoldValueChanged?.Invoke(goldAmount);

        Debug.Log($"Setting gold to: {goldAmount}");
    }
}
