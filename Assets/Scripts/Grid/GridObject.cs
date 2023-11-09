using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;
    private ItemObject _itemObject;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
    }

    public void SetItemObject(ItemObject item)
    {
        _itemObject = item;
    }

    public ItemObject GetItemObject()
    {
        return _itemObject;
    }

    public GridPosition GetGridPosition() 
    { 
        return _gridPosition; 
    }

    public override string ToString()
    {
        return _gridPosition.ToString() + "\n" + _itemObject;
    }
}
