using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationUpgrader : MonoBehaviour
{
    [field: SerializeField] public OrderItem orderItem { get; private set; }

    [field: SerializeField] public List<Workstation> workstations { get; private set; }

    [field: SerializeField] public bool hasWorkstation { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //AddNewWorkstation();
            // IncreaseCostMultiplier();
            // DecreaseSpeedMultiplier();
        }
    }

    void IncreaseCostMultiplier()
    {
        Modifiers.Instance.IncreaseCostMultiplier(orderItem);
    }

    void DecreaseSpeedMultiplier()
    {
        Modifiers.Instance.DecreaseSpeedMultiplier(orderItem);
    }

    void AddNewWorkstation()
    {
        hasWorkstation = true;

        foreach (Workstation workstation in workstations)
        {
            if (!workstation.gameObject.activeInHierarchy)
            {
                workstation.gameObject.SetActive(true);
                return;
            }
        }
    }
}
