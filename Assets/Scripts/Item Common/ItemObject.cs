using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IDragObject
{
    private SO_ZbrojkoItem _itemReference;
    private ItemType _itemType;
    private SO_GameObjectReference _gameObjectReference;

    private Vector3 _position; //testing only

    public void Setup(SO_ZbrojkoItem item)
    {
        _itemReference = item;
        _itemType = item.ItemType;
        _gameObjectReference = item.ItemPrefabReference;
    }

    public override string ToString()
    {
        return _itemType.ToString();
    }

    // ******************** GETTER FUNCTIONS ********************

    public SO_ZbrojkoItem GetItemReference()
    {
        return _itemReference;
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }

    public SO_GameObjectReference GetObjectReference()
    {
        return _gameObjectReference;
    }

    // ******************** INTERFACE FUNCTIONS ********************
    public void OnStartDrag()
    {
        _position = transform.position;
        //fire event
        //open the destination area
    }

    public void OnDrag()
    {
        //potentially unneccessary
    }

    public void OnEndDrag()
    {
        transform.position = _position;

        //fire event
        //check if above destination area
        //if yes trigger animation or something, clear the grid position, deduct value
        //if no, return to start position
    }
}
