using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //should probably also be done through an SO?
    private SO_ZbrojkoItem _itemReference;
    private ItemType _itemType;
    private SO_GameObjectReference _gameObjectReference;

    private void Start()
    {
        
    }

    public void Setup(SO_ZbrojkoItem item)
    {
        _itemReference = item;
        _itemType = item.ItemType;
        _gameObjectReference = item.ItemPrefabReference;
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

    public SO_GameObjectReference GetObjectReference()
    {
        return _gameObjectReference;
    }
}
