using System;
using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour , IInteractable
{
    [SerializeField] private ButtonOperation _buttonOperation;

    public UnityEvent<ButtonOperation> OnClickEvent = new UnityEvent<ButtonOperation>();


    private void OnEnable()
    {
        InputHandler.OnMouseDownAction += TryInteract;
    }


    private void OnDisable()
    {
        InputHandler.OnMouseDownAction -= TryInteract;
    }

    [ContextMenu("On Click")]
    public void OnClick()
    {
        OnClickEvent?.Invoke(_buttonOperation);
    }

    public ButtonOperation GetButtonOperation()
    {
        return _buttonOperation;
    }

    public void TryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(MouseController.Instance.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            Interact();
        }
    }
    public void Interact()
    {
        OnClick();
    }
    
}
