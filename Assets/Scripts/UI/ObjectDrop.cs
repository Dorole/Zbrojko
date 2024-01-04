using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDrop : MonoBehaviour, IObjectDrop
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemType _itemType;

    private void Start()
    {
        _image.enabled = false;

        DragObject.OnObjectPicked += DragObject_OnObjectPicked;
        DragObject.OnObjectDropped += DragObject_OnObjectDropped;
    }

    //use tweening for this, see YT videos
    private void DragObject_OnObjectPicked(ItemType itemType)
    {
        if (itemType == _itemType)
            _image.enabled = true;
    }

    private void DragObject_OnObjectDropped(ItemType itemType)
    {
        if (itemType == _itemType)
            _image.enabled = false;
    }

    private void OnDestroy()
    {
        DragObject.OnObjectPicked -= DragObject_OnObjectPicked;
        DragObject.OnObjectDropped -= DragObject_OnObjectDropped;
    }

}
