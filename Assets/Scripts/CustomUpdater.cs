using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CustomUpdater : MonoBehaviour, GameInput.IBatterNRunnerActions
{
    public event UnityAction<Vector2> MoveActions;
    public event UnityAction SwingActions;
    public event UnityAction<bool> BuntActions;

    private bool m_IsMKeyPressed = false;
    private bool m_IsNKeyPressed = false;
    private bool m_IsKKeyPressed = false;
    private bool m_IsJKeyPressed = false;
    private bool m_IsHKeyPressed = false;

    private GameInput m_GameInput;

    public void Update()
    {
        // Handle movement keys
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_IsMKeyPressed = true;
            OnMove(new InputAction.CallbackContext());
        }
        else if (Input.GetKeyUp(KeyCode.M) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_IsMKeyPressed = false;
            OnMove(new InputAction.CallbackContext());
        }

        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.LeftApple))
        {
            m_IsNKeyPressed = true;
            OnMove(new InputAction.CallbackContext());
        }
        else if (Input.GetKeyUp(KeyCode.N) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_IsNKeyPressed = false;
            OnMove(new InputAction.CallbackContext());
        }

        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_IsKKeyPressed = true;
            OnMove(new InputAction.CallbackContext());
        }
        else if (Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_IsKKeyPressed = false;
            OnMove(new InputAction.CallbackContext());
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_IsJKeyPressed = true;
            OnMove(new InputAction.CallbackContext());
        }
        else if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_IsJKeyPressed = false;
            OnMove(new InputAction.CallbackContext());
        }

        // Handle swing key
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            m_IsHKeyPressed = true;
            OnSwing(new InputAction.CallbackContext());
        }
        else if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            m_IsHKeyPressed = false;
        }
    }

    public void OnEnable()
    {
        if (m_GameInput == null)
        {
            m_GameInput = new GameInput();
            m_GameInput.BatterNRunner.SetCallbacks(this);
        }
        m_GameInput.BatterNRunner.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (MoveActions != null)
        {
            Vector2 moveValue = Vector2.zero;

            if (m_IsMKeyPressed)
            {
                moveValue = Vector2.one; // Move in a specific direction when "M" is pressed
            }
            else if (m_IsNKeyPressed)
            {
                moveValue = new Vector2(-1f, 0f); // Move left when "N" is pressed
            }
            else if (m_IsKKeyPressed)
            {
                moveValue = new Vector2(0f, 1f); // Move upwards when "K" is pressed
            }
            else if (m_IsJKeyPressed)
            {
                moveValue = new Vector2(0f, -1f); // Move downwards when "J" is pressed
            }
            else
            {
                moveValue = context.ReadValue<Vector2>(); // Use the actual input value
            }

            MoveActions.Invoke(moveValue);
            Debug.Log(moveValue);
        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (SwingActions != null && m_IsHKeyPressed)
        {
            SwingActions.Invoke();
            Debug.Log("BatSwing");
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
