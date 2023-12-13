using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //should probably also be done through an SO?
    private SO_ZbrojkoItem _itemReference;
    private ItemType _itemType;

    private void Start()
    {
        
    }

    public void Setup(SO_ZbrojkoItem item)
    {
        _itemReference = item;
        _itemType = item.ItemType;
    }

    public SO_ZbrojkoItem GetItemReference()
    {
        return _itemReference;
    }

    public override string ToString()
    {
        return _itemType.ToString();
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }
}
