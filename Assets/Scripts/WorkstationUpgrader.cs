using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkstationUpgrader : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public OrderItem orderItem { get; private set; }

    [field: SerializeField] public List<Workstation> workstations { get; private set; }

    [field: SerializeField] public bool hasWorkstation { get; private set; } = false;

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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Game Object Clicked!");
        UpgradeWorkstationWindow.Instance.OpenWindow(this);
    }
}
