using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragObject : MonoBehaviour, IDragObject
{
    public static event Action<ItemType> OnObjectPicked;
    public static event Action<ItemType> OnObjectDropped;
    public Action<bool> OnEndDrag { get; set; }

    private Vector3 _startPosition; //testing only
    private ItemType _itemType;

    private void Start()
    {
        OnEndDrag = HandleDropCompleted;
        ItemObject itemObject = GetComponent<ItemObject>();
        _itemType = itemObject.GetItemType();
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
        }
        else
        {
            Debug.Log("Return to pos");
            transform.position = _startPosition;
        }

        OnObjectDropped?.Invoke(_itemType);
    }

}
