using RPG.Core.UI.Dragging;
using System.Dynamic;
using UnityEngine;

/// <summary>
/// Place on UI slots that hold number item icons
/// </summary>
public class UI_NumberSlot : MonoBehaviour, IDragSource<SO_ZbrojkoItem>
{
    [SerializeField] private int _index = default;
    [SerializeField] private SO_Number _numberItem = default;
    [SerializeField] private ZbrojkoItemIcon _icon = default;

    private NumbersManager _numbersManager = default;

    public void Setup(NumbersManager numbersManager, int index)
    {
        _numbersManager = numbersManager;
        _index = index;
        _numberItem = _numbersManager.GetNumberItem(_index);
        _icon.SetItem(_numberItem);
    }

    public void AddItems(SO_ZbrojkoItem item, int number) {} //imported interface; refactor

    public SO_ZbrojkoItem GetItem()
    {
        return _numbersManager.GetNumberItem(_index);
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
        Debug.Log("Used number " + _numberItem.Value.ToString());
    }
}
