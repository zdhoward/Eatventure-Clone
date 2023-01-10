using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workstation : MonoBehaviour
{
    float gridSize = 10f;

    [field: SerializeField] public Transform CookTransform { get; private set; }

    public OrderItem orderItem;

    public bool isBusy;

    void OnDrawGizmos()
    {
        // Cook position
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * gridSize, Vector3.one * gridSize);
    }
}
