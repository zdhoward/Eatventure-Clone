using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;

    [SerializeField] GameObject cookPrefab;
    [SerializeField] Transform spawnPoint;

    // List<Workstation> workstations = new List<Workstation>();
    // List<Servingstation> servingstations = new List<Servingstation>();

    [field: SerializeField] public List<Workstation> workstations { get; private set; }
    [field: SerializeField] public List<Servingstation> servingstations { get; private set; }

    List<CookStateMachine> cooks = new List<CookStateMachine>();

    List<Order> pendingOrders = new List<Order>();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one CookManager in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // FindAllServingstations();
        // FindAllWorkstations();
    }

    void Start()
    {
    }

    void Update()
    {
        if (cooks.Count < Modifiers.Instance.maxCooks)
            SpawnNewCook();

        ManageOrders();
    }

    void SpawnNewCook()
    {
        GameObject cookGameObject = Instantiate(cookPrefab, spawnPoint.position, Quaternion.identity);
        CookStateMachine cook = cookGameObject.GetComponent<CookStateMachine>();
        cooks.Add(cook);
    }

    public void ReceiveOrder(Order order)
    {
        pendingOrders.Add(order);
    }

    void ManageOrders()
    {
        if (pendingOrders.Count > 0)
        {
            foreach (CookStateMachine cook in cooks)
            {
                if (cook.isIdle)
                {
                    Workstation availableWorkstation = FindAvailableWorkstation(pendingOrders[0].orderItem);
                    if (availableWorkstation == null)
                        continue;

                    cook.AssignWorkstation(availableWorkstation);
                    cook.CookOrder(pendingOrders[0]);
                    pendingOrders.RemoveAt(0);
                    return;
                }
            }
        }
    }

    public Servingstation FindServingstation()
    {
        int servingstationIndex = Random.Range(0, servingstations.Count);
        return servingstations[servingstationIndex];
    }

    public Workstation FindAvailableWorkstation(OrderItem orderItem)
    {
        // Debug.Log("Find Available Workstation out of " + workstations.Count);
        foreach (Workstation workstation in workstations)
        {
            if (!workstation.gameObject.activeInHierarchy)
            {
                // Debug.Log(workstation.name + " is not active");
                continue;
            }

            if (!workstation.isBusy && workstation.orderItem == orderItem)
            {
                workstation.isBusy = true;
                return workstation;
            }
        }

        //Debug.LogError("Workstation could not be found");
        return null;
    }

    // void FindAllServingstations()
    // {
    //     GameObject[] servingstationGameObjects = GameObject.FindGameObjectsWithTag("Servingstation");
    //     foreach (GameObject servingstationGameObject in servingstationGameObjects)
    //     {
    //         servingstations.Add(servingstationGameObject.GetComponent<Servingstation>());
    //     }
    // }

    // void FindAllWorkstations()
    // {
    //     GameObject[] workstationGameObjects = GameObject.FindGameObjectsWithTag("Workstation");
    //     foreach (GameObject workstationGameObject in workstationGameObjects)
    //     {
    //         workstations.Add(workstationGameObject.GetComponent<Workstation>());
    //     }
    // }
}
