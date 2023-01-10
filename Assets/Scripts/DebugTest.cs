using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    List<string> testList = new List<string> { "One", "Two", "Three" };

    // Start is called before the first frame update
    void Start()
    {
        double number = 12.756;

        Debug.Log(number);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
