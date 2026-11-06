using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    
    [SerializeField] private GameObject _mouse;
    [SerializeField] private float _mouseSpeed;
    
    public static MouseController Instance; 
    
    private Vector2 _mousePosition;
    private Vector2 _mouseDelta;

    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        Instance = this;
        DontDestroyOnLoad(this);
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        InputHandler.SetMousePosition += SetMousePosition;
        InputHandler.OnChangedDeltaAction += SetMouseDelta;
    }

    private void OnDisable()
    {
        InputHandler.SetMousePosition -= SetMousePosition;
        InputHandler.OnChangedDeltaAction -= SetMouseDelta;
    }

    private void SetMousePosition(Vector2 position) => _mousePosition = position;
    private void SetMouseDelta(Vector2 delta) => _mouseDelta = delta;
    public Vector2 GetMousePosition() => _mousePosition;
    public Vector2 GetMouseDelta() => _mouseDelta;

    private void Update()
    {
        
        Vector2 lerped = Vector2.Lerp(_mouse.transform.position, _mousePosition, _mouseSpeed * Time.deltaTime);
        _mouse.transform.position = new Vector3(lerped.x , lerped.y, 0);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * _mouseSpeed);
    }   
}
