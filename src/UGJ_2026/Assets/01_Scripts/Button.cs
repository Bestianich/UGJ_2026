using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour , IInteractable
{
    [SerializeField] private ButtonOperation _buttonOperation;

    public UnityEvent<ButtonOperation> OnClickEvent = new UnityEvent<ButtonOperation>();
    
    [ContextMenu("On Click")]
    public void OnClick()
    {
        OnClickEvent?.Invoke(_buttonOperation);
    }

    public ButtonOperation GetButtonOperation()
    {
        return _buttonOperation;
    }

    public void Interact()
    {
        OnClick();
    }
    
}
