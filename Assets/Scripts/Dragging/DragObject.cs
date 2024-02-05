using System;
using UnityEngine;

public class DragObject : MonoBehaviour, IDragObject
{
    public static event Action<ItemType> s_OnObjectPicked;
    public static event Action<ItemType> s_OnObjectDropped;
    public static event Action<ItemObject> s_OnObjectRemoved;
    public event Action OnObjectPicked;
    public event Action OnObjectDropped;
    public event Action OnObjectRemoved;

    public Action<bool> OnEndDrag { get; set; }

    private Vector3 _startPosition;
    private ItemObject _itemObject;
    private ItemType _itemType;

    private void Start()
    {
        OnEndDrag = HandleDropCompleted;

        _itemObject = GetComponent<ItemObject>();
        _itemType = _itemObject.GetItemType();
    }

    // ******************** INTERFACE FUNCTIONS ********************
    public void OnStartDrag()
    {
        _startPosition = transform.position;

        s_OnObjectPicked?.Invoke(_itemType);
        OnObjectPicked?.Invoke();
    }

    public void OnDrag()
    {      
    }

    private void HandleDropCompleted(bool isOverTarget)
    {
        if (isOverTarget)
        {
            Debug.Log("Dropped over target!");
            s_OnObjectRemoved?.Invoke(_itemObject);
            OnObjectRemoved?.Invoke();
        }
        else
        {
            transform.position = _startPosition;
        }

        s_OnObjectDropped?.Invoke(_itemType);
        OnObjectDropped?.Invoke();
    }

}
