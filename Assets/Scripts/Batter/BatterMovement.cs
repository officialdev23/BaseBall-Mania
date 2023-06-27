using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterMovement : MonoBehaviour
{
    GameInput gameInput;
    public bool useOldInputSystem;
    private void Awake()
    {
        gameInput = new GameInput();
    }

    private void OnEnable()
    {
        gameInput.Enable();
    }

    private void OnDisable()
    {
        gameInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (useOldInputSystem) oldInputSystem();
        else
        {
            NewInputSystem();
        }
        // Debug.Log("This is batter controller");
    }

    void oldInputSystem()
    {
        Debug.Log("This is old");
    }

    void NewInputSystem()
    {
        Debug.Log("This is new");
    }
}
