using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowsOnClick : MonoBehaviour
{
    public static CloseWindowsOnClick Instance;

    [SerializeField] GameObject button;
    [SerializeField] Window[] windows;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one CloseWindowsOnClick in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void WindowOpened()
    {
        button.SetActive(true);
    }

    public void CloseAllWindows()
    {
        foreach (Window window in windows)
            window.CloseWindow();

        button.SetActive(false);
    }
}
