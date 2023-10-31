using RPG.Core.UI.Dragging;
using System.Dynamic;
using UnityEngine;

public class UI_NumberSlot : MonoBehaviour, IDragSource<SO_ZbrojkoItem>
{
    [SerializeField] private int _index = 0;
    [SerializeField] private ZbrojkoItemIcon _icon = default;

    private NumbersManager _numbersManager = default;

    public void Setup(NumbersManager numbersManager, int index)
    {
        _numbersManager = numbersManager;
        _index = index;
        _icon.SetItem(_numbersManager.GetNumberItem(_index));
    }

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        //outside of this: create Numbers class which will keep track of available numbers and which number is where 
        //call function in Numbers class which will set item as this slot's item and trigger an event

        //set item and its icon as this slot's item and icon
        //don't think number argument is neccesary

        //necessary even?

    }

    public SO_ZbrojkoItem GetItem()
    {
        //return the result of a function from the Numbers class which returns the item in this slot (by slot index)
        return _numbersManager.GetNumberItem(_index);
    }

    public int GetNumber()
    {
        //realno ovo nije ni potrebno, osobito za brojeve, ali neka stoji za sad...
        //eventualno refactor da vrati value, a ne number jer je number uvijek 0
        //ali pazi onda sto se dogodi u DragItem!
        if (GetItem() != null)
            return 1;
        else
            return 0;
    }

    public void RemoveItems(int number)
    {
        //call Numbers.RemoveItem(int slotIndex)
        //set slot's item to null - not needed?? instead -->
        //trigger routine for redrawing slot's UI --> only after spawning the number
        throw new System.NotImplementedException();
    }
}
