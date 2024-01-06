using RPG.Core.UI.Dragging;
using System;
using UnityEngine;

/// <summary>
/// Place only on destinations for 2D sprites. 
/// This is where a 3D object spawns when the sprite is dragged here.
/// </summary>
public class DropTarget : MonoBehaviour, IDragDestination<SO_ZbrojkoItem>
{   
    [SerializeField] private ItemType _acceptableItemType = ItemType.Number;
    [SerializeField] private int _maxAcceptable = 10;
    [SerializeField] private LevelGrid _levelGrid;

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        if (_acceptableItemType != item.ItemType)
        {
            Debug.Log("Item types don't match.");
            return;
        }

        _levelGrid.AddItemToGrid(item);
    }

    public int MaxAcceptable(SO_ZbrojkoItem item)
    {
        //make use of this!
        return _maxAcceptable;
    }


}
