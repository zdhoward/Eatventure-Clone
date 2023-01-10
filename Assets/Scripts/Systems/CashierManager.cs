using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    public static CashierManager Instance;

    [SerializeField] GameObject cashierPrefab;
    [SerializeField] Transform spawnPoint;

    List<CashierStateMachine> cashiers = new List<CashierStateMachine>();

    List<Cashierstation> cashierstations = new List<Cashierstation>();
    List<Servingstation> servingstations = new List<Servingstation>();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one CashierManager in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        FindAllCashierstations();
        FindAllServingstations();
    }

    void Start()
    {
    }

    public void SpawnNewCashier()
    {
        GameObject cashierGameObject = Instantiate(cashierPrefab, spawnPoint.position, Quaternion.identity);
        CashierStateMachine cashier = cashierGameObject.GetComponent<CashierStateMachine>();
        cashiers.Add(cashier);
    }

    void Update()
    {
        if (cashiers.Count < Modifiers.Instance.maxCashiers)
            SpawnNewCashier();

        ManageOrders();
    }

    void ManageOrders()
    {
        TakeAvailableOrders();
        RetrieveAvailableOrders();
    }

    void TakeAvailableOrders()
    {
        foreach (Cashierstation cashierstation in cashierstations)
        {
            if (cashierstation.hasCustomer && !cashierstation.isReservedByCashier)
            {
                foreach (CashierStateMachine cashier in cashiers)
                {
                    if (cashier.isIdle)
                    {
                        cashier.TakeOrder(cashierstation);
                        return;
                    }
                }
            }
        }
    }

    void RetrieveAvailableOrders()
    {
        foreach (Servingstation servingstation in servingstations)
        {
            if (servingstation.hasOrder && !servingstation.isReservedByCashier)
            {
                foreach (CashierStateMachine cashier in cashiers)
                {
                    if (cashier.isIdle)
                    {
                        cashier.RetreiveOrder(servingstation);
                        return;
                    }
                }
            }
        }
    }

    public Cashierstation FindCashierstationOfOrder(Order order)
    {
        foreach (Cashierstation cashierstation in cashierstations)
        {
            if (cashierstation.order.orderID == order.orderID)
            {
                return cashierstation;
            }
        }

        Debug.LogError("OrderID not found for order: " + order.orderID);

        return null;
    }

    void FindAllCashierstations()
    {
        GameObject[] cashierstationGameObjects = GameObject.FindGameObjectsWithTag("Cashierstation");
        foreach (GameObject cashierstationGameObject in cashierstationGameObjects)
        {
            cashierstations.Add(cashierstationGameObject.GetComponent<Cashierstation>());
        }
    }

    void FindAllServingstations()
    {
        GameObject[] servingstationGameObjects = GameObject.FindGameObjectsWithTag("Servingstation");
        foreach (GameObject servingstationGameObject in servingstationGameObjects)
        {
            servingstations.Add(servingstationGameObject.GetComponent<Servingstation>());
        }
    }
}
