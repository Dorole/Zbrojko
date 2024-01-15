using RPG.Core.UI.Dragging;
using UnityEngine;

/// <summary>
/// Place only on destinations for 2D sprites. 
/// This is where a 3D object spawns when the sprite is dragged here.
/// </summary>
public class DropTarget : MonoBehaviour, IDragDestination<SO_ZbrojkoItem>
{   
    [SerializeField] private ItemType _acceptableItemType = ItemType.Number;
    [SerializeField] private LevelGrid _levelGrid;

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        if (_acceptableItemType != item.ItemType || !_levelGrid.HasAvailablePositions())
        {
            return;
        }

        _levelGrid.AddItemToGrid(item);
     }

    public int MaxAcceptable(SO_ZbrojkoItem item)
    {
        return _levelGrid.GetTotalAvailablePositions();
    }
}
