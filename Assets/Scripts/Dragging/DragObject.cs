using System;
using UnityEngine;

public class DragObject : MonoBehaviour, IDragObject
{
    public static event Action<ItemType> OnObjectPicked;
    public static event Action<ItemType> OnObjectDropped;
    public static event Action<ItemObject> OnObjectRemoved;
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
        OnObjectPicked?.Invoke(_itemType);
    }

    public void OnDrag()
    {
        //potentially just trigger animation
    }

    private void HandleDropCompleted(bool isOverTarget)
    {
        if (isOverTarget)
        {
            Debug.Log("Dropped over target!");
            OnObjectRemoved?.Invoke(_itemObject);
        }
        else
        {
            transform.position = _startPosition;
        }

        OnObjectDropped?.Invoke(_itemType);
    }

}
