using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour , PlayerInput.IInGameActions
{
    public delegate void OnChangedDelta(Vector2 delta);
    public static event OnChangedDelta OnChangedDeltaAction;
    
    public delegate void OnMouseMove(Vector2 position);
    public static event OnMouseMove SetMousePosition;

    public delegate void OnMouseDown();
    public static event OnMouseDown OnMouseDownAction;
    
    public delegate void OnMouseUp();
    public static event OnMouseUp OnMouseUpAction;

    private PlayerInput _controls;

    private void Awake()
    {
        _controls = new PlayerInput();
        _controls.InGame.SetCallbacks(this);
        _controls.InGame.Enable();
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnChangedDeltaAction?.Invoke(context.ReadValue<Vector2>());
        else if (context.canceled)
            OnChangedDeltaAction?.Invoke(Vector2.zero);
    }

    public void OnMoveMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetMousePosition?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnMouseDownAction?.Invoke();
        if(context.canceled)
            OnMouseUpAction?.Invoke();
    }
    
}
