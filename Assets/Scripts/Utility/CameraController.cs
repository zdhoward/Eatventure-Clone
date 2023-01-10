using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float moveSpeed = 40f;

    float minZ = -41f;
    float maxZ = -14f;

    float dragSpeed = 2f;
    Vector3 dragOrigin;

    Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        HandleMovementWithWASD();
        HandleMovementClickAndDrag();
    }

    void HandleMovementWithWASD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Scroll up
            float z = transform.position.z;
            z += moveSpeed * Time.deltaTime;
            z = Mathf.Min(maxZ, z);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Scroll down
            float z = transform.position.z;
            z -= moveSpeed * Time.deltaTime;
            z = Mathf.Max(minZ, z);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }

    void HandleMovementClickAndDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector3 pos = mainCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0, 0, -pos.y * dragSpeed);

        transform.Translate(move, Space.World);

        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));
    }
}
