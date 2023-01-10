using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    [SerializeField] GameObject customerPrefab;
    [field: SerializeField] public Transform SpawnPoint { get; private set; }

    [field: SerializeField] public List<WorkstationUpgrader> workstationUpgraders { get; private set; }

    List<Cashierstation> cashierstations = new List<Cashierstation>();

    List<CustomerStateMachine> customers = new List<CustomerStateMachine>();

    public List<OrderItem> orderItems;

    int nextOrderID = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one CustomerManager in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        FindAllCashierstations();
    }

    void Start()
    {
    }

    void Update()
    {
        UpdateAvailableOrderItems();

        if (customers.Count < Modifiers.Instance.maxCustomers)
            SpawnNewCustomer();
    }

    void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(customerPrefab, SpawnPoint.position, Quaternion.identity);
        CustomerStateMachine customer = customerGameObject.GetComponent<CustomerStateMachine>();
        customer.AssignCashierstation(FindFreeCashierstation());
        customer.AssignOrder(GenerateOrder(customer));
        customers.Add(customer);
    }

    Order GenerateOrder(CustomerStateMachine customer)
    {
        int itemToOrderIndex = Random.Range(0, orderItems.Count);
        OrderItem itemToOrder = orderItems[itemToOrderIndex];
        Order order = new Order(nextOrderID, itemToOrder, 1, customer);
        nextOrderID++;
        return order;
    }

    Cashierstation FindFreeCashierstation()
    {
        foreach (Cashierstation cashierstation in cashierstations)
        {
            if (!cashierstation.isReservedByCustomer)
            {
                return cashierstation;
            }
        }

        Debug.LogError("No free Cashierstations");
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

    public void DespawnCustomer(CustomerStateMachine customer)
    {
        customers.Remove(customer);
    }

    void UpdateAvailableOrderItems()
    {
        foreach (WorkstationUpgrader upgrader in workstationUpgraders)
        {
            if (!upgrader.hasWorkstation)
                continue;

            if (!orderItems.Contains(upgrader.orderItem))
            {
                orderItems.Add(upgrader.orderItem);
            }
        }
    }
}
