using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour
{
    [SerializeField] private ButtonOperation _buttonOperation;

    public UnityEvent<ButtonOperation> OnClickEvent = new UnityEvent<ButtonOperation>();
    
    [ContextMenu("On Click")]
    public void OnClick()
    {
        OnClickEvent?.Invoke(_buttonOperation);
    }
}
