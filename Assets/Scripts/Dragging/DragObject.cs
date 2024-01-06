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
            //trigger animation or something, clear the grid position, deduct value
            //Check LevelGrid > RemoveAllItems
            //1.Clear grid pos
            //2.Trigger animation or whatever > mozda coroutine i onda na kraju return to pool?
            //3.Return to pool
            OnObjectRemoved?.Invoke(_itemObject);
        }
        else
        {
            transform.position = _startPosition;
        }

        OnObjectDropped?.Invoke(_itemType);
    }

}
