using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragItem_3DObject : MonoBehaviour
{
    [SerializeField] private InputAction _objectPress;
    [SerializeField] private InputAction _pressScreenPosition;
    [SerializeField] private float _mouseDragPhysicsSpeed = 10;
    [SerializeField] private float _mouseDragSpeed = 0.1f;

    private Camera _mainCamera;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector3 _currentScreenPosition;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _pressScreenPosition.Enable();
        _objectPress.Enable();

        _pressScreenPosition.performed += _pressScreenPosition_performed;
        _objectPress.performed += _mouseClick_performed;
        
    }

    private void OnDisable()
    {
        _pressScreenPosition.performed -= _pressScreenPosition_performed;
        _objectPress.performed -= _mouseClick_performed;

        _pressScreenPosition.Disable();
        _objectPress.Disable();
    }
    
    private void _pressScreenPosition_performed(InputAction.CallbackContext context)
    {
        _currentScreenPosition = context.ReadValue<Vector2>();
    }

    private void _mouseClick_performed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(_currentScreenPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && (hit.collider.gameObject.GetComponent<IDragObject>() != null))
            {
                StartCoroutine(CO_DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator CO_DragUpdate(GameObject clickedObject)
    {
        //need Rigidbody? potencijalno da, inace collider ne funkcionira?
        clickedObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody);
        clickedObject.TryGetComponent<IDragObject>(out IDragObject dragObject);

        dragObject?.OnStartDrag();
        
        float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);

        //call onDrag() inside the loop
        while (_objectPress.ReadValue<float>() != 0)
        {
            Ray ray = _mainCamera.ScreenPointToRay(_currentScreenPosition);
            dragObject.OnDrag();

            if (rigidbody != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rigidbody.velocity = direction * _mouseDragPhysicsSpeed;
                yield return _waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref _velocity, _mouseDragSpeed);
                yield return null;
            }
        }

        dragObject?.OnEndDrag?.Invoke(IsPointerOverDropTarget());        
    }

    private bool IsPointerOverDropTarget()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = _currentScreenPosition;

        List<RaycastResult> hitResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hitResults);

        if (hitResults.Count > 0)
        {
            foreach(RaycastResult hitResult in hitResults)
            {
                if (hitResult.gameObject.TryGetComponent<IObjectDrop>(out IObjectDrop dropTarget))
                    return true;
            }
        }

        return false;
    }
}
