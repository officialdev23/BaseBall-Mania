using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Battter Input", menuName = "InputReader/Batter")]
public class BatterInputReader : ScriptableObject, GameInput.IBatterNRunnerActions
{

    public static BatterInputReader Instance { get; private set; }
    public event UnityAction<Vector2> MoveActions;
    public event UnityAction SwingActions;
    public event UnityAction<bool> BuntActions;
    public bool useOldInputSystem;

    private GameInput m_GameInput;

    public void OnEnable()
    {
        if (m_GameInput == null)
        {
            m_GameInput = new GameInput();
            m_GameInput.BatterNRunner.SetCallbacks(this);

        }
        m_GameInput.BatterNRunner.Enable();
    }

    public void Awake()
    {
        Instance = this;
    }
    public void OnDisable()
    {
        m_GameInput.BatterNRunner.Disable();
    }

    private float updateInterval = 1.0f;
    private float timer = 0.0f;

    public void CustomUpdate()
    {
        // Perform update logic here
        Debug.Log("CustomUpdate called.");

        // Simulate an update loop
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            // Execute update logic at a specific interval
            timer = 0.0f;
            Debug.Log("Interval reached.");
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (MoveActions != null)
        {
            MoveActions.Invoke(context.ReadValue<Vector2>());
            Debug.Log(context.ReadValue<Vector2>());
        }
    }
    public void OnSwing(InputAction.CallbackContext context)
    {
        if (SwingActions != null)
        {
            SwingActions.Invoke();
            //Debug.Log("BatSwing");
        }
    }
    public void OnBunt(InputAction.CallbackContext context)
    {
        if (MoveActions != null)
        {
            if (context.performed)
            {
                BuntActions.Invoke(true);
            }
            else if (context.canceled)
            {
                BuntActions.Invoke(false);
            }
        }
    }
}







//using UnityEngine;
//using UnityEngine.Events;

//[CreateAssetMenu(fileName = "New Battter Input", menuName = "InputReader/Batter")]
//public class BatterInputReader : ScriptableObject
//{
//    public event UnityAction<Vector2> MoveActions;
//    public event UnityAction SwingActions;
//    public event UnityAction<bool> BuntActions;

//    public void OnEnable()
//    {
//        // No changes required in this method
//    }

//    public void OnDisable()
//    {
//        // No changes required in this method
//    }

//    public void Update()
//    {
//        // Handle movement
//        float moveX = 0f;
//        float moveY = 0f;

//        if (Input.GetKey(KeyCode.A))
//        {
//            moveX = -1f;
//        }
//        else if (Input.GetKey(KeyCode.D))
//        {
//            moveX = 1f;
//        }

//        if (Input.GetKey(KeyCode.W))
//        {
//            moveY = 1f;
//        }
//        else if (Input.GetKey(KeyCode.S))
//        {
//            moveY = -1f;
//        }

//        Vector2 moveDirection = new Vector2(moveX, moveY);
//        if (MoveActions != null)
//        {
//            MoveActions.Invoke(moveDirection);
//            Debug.Log("PlayerMoved");
//        }

//        // Handle swing action
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            if (SwingActions != null)
//            {
//                SwingActions.Invoke();
//                Debug.Log("BatSwing");
//            }
//        }

//        // Handle bunt action
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            if (BuntActions != null)
//            {
//                BuntActions.Invoke(true);
//            }
//        }
//        else if (Input.GetKeyUp(KeyCode.B))
//        {
//            if (BuntActions != null)
//            {
//                BuntActions.Invoke(false);
//            }
//        }
//    }
//}

