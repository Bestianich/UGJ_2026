using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DraggableEntity : MonoBehaviour
{
    [SerializeField] private float _launchForce = 5f;
    [SerializeField] private float _offsetZ = 5f; //The distance from the camera
    
    private Rigidbody _rb;
    private bool _isGettingDragged = false;
    

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
            
            _rb.freezeRotation = true;
            _rb.isKinematic = false;
            _rb.useGravity = false;
        }
        
    }

    private void Drag()
    {
        Vector3 mousePos = MouseController.Instance.GetMousePosition();
      
        Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, _offsetZ));
        _rb.MovePosition(mouseWorldPos);
        
    }
    


    private void EndDrag()
    {
        _isGettingDragged = false;
        _rb.freezeRotation = false;
        _rb.useGravity = true;
        Vector3 mouseDelta = MouseController.Instance.GetMouseDelta();
        Vector3 launchVelocity = new Vector3(mouseDelta.x, mouseDelta.y, 0) * _launchForce;

        _rb.AddForce(Vector3.forward * _launchForce, ForceMode.Impulse);
        _rb.linearVelocity = launchVelocity;
    }

    

    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _offsetZ);
    }
}
