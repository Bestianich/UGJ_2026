using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DraggableEntity : MonoBehaviour
{
    [SerializeField] private float _dragPrecision = 0.1f;
    [SerializeField] private float _launchForce = 5f;
    
    private Rigidbody _rb;
    private bool _isGettingDragged = false;

    private Vector3 _offset;
    private float _offsetZ = 1f;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }
    private void OnEnable()
    {
        InputHandler.OnMouseDownAction += TryStartDrag; 
        InputHandler.OnMouseUpAction += EndDrag;
    }

    private void OnDisable()
    {
        InputHandler.OnMouseDownAction -= TryStartDrag;
        InputHandler.OnMouseUpAction -= EndDrag;
    }


    private void Update()
    {
        if(!_isGettingDragged)
            return;
        Drag();
    }
    
    private void TryStartDrag()
    {
        
        Ray ray = _camera.ScreenPointToRay(MouseController.Instance.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            _isGettingDragged = true;
            Vector2 mousePos = MouseController.Instance.GetMousePosition();
            
            _offsetZ = hit.distance;

            Vector3 mouseWorld = _camera.ScreenToWorldPoint(
                new Vector3(mousePos.x, mousePos.y, _offsetZ));
            
            _offset = transform.position - mouseWorld;
            
            _rb.freezeRotation = true;
            _rb.isKinematic = false;

        }
        
    }

    private void Drag()
    {
        Vector3 mousePos = MouseController.Instance.GetMousePosition();
      
        Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, _offsetZ));
        
        Vector3 snappedPosition = SnapToMouse(mouseWorldPos + _offset);
        _rb.MovePosition(snappedPosition);
    }
    


    private void EndDrag()
    {
        _isGettingDragged = false;
        _rb.freezeRotation = false;
        
        Vector3 mouseDelta = MouseController.Instance.GetMouseDelta();
        Vector3 launchVelocity = new Vector3(mouseDelta.x, mouseDelta.y, 0) * _launchForce;
        
        _rb.linearVelocity = launchVelocity;
    }


    private Vector3 SnapToMouse(Vector3 position)
    {
        return new Vector3(
                Mathf.Round(position.x / _dragPrecision) * _dragPrecision,
                Mathf.Round(position.y / _dragPrecision) * _dragPrecision,
                position.z
            );
    }
}
