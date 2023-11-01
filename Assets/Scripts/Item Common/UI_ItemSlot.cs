using RPG.Core.UI.Dragging;
using System.Dynamic;
using UnityEngine;

/// <summary>
/// Place on UI slots that hold item icons
/// </summary>
public class UI_ItemSlot : MonoBehaviour, IDragSource<SO_ZbrojkoItem>
{
    [SerializeField] private int _index = default;
    [SerializeField] private SO_ZbrojkoItem _item = default;
    [SerializeField] private ZbrojkoItemIcon _icon = default;

    private ItemManager _itemManager = default;

    public void Setup(ItemManager itemManager, int index)
    {
        _itemManager = itemManager;
        _index = index;
        _item = _itemManager.GetItemBySlotIndex(_index);
        _icon.SetItem(_item);
    }

    public void AddItems(SO_ZbrojkoItem item, int number) {} //imported interface; refactor

    public SO_ZbrojkoItem GetItem()
    {
        return _itemManager.GetItemBySlotIndex(_index);
    }

    public int GetNumber()
    {
        //only needed for DragItem calculations
        if (GetItem() != null)
            return 1;
        else
            return 0;
    }

    public void RemoveItems(int number)
    {
        //call Numbers.RemoveItem(int slotIndex)
        //trigger routine for redrawing slot's UI --> only after spawning the number event
        Debug.Log("Used value " + _item.Value.ToString());
    }
}
