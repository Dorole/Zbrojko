using UnityEngine;

public class NumbersManager : ItemManager
{
    [SerializeField]
    private ItemSlot[] _numberSlots = default;

    public override SO_ZbrojkoItem GetItemBySlotIndex(int index)
    {
        return _numberSlots[index].Item;
    }

    private void Awake() 
    {
        AssignIndicesToSlots();
    }

    protected override void AssignIndicesToSlots()
    {
        for (int i = 0; i < _numberSlots.Length; i++)
        {
            _numberSlots[i].Index = i;
        }
    }
}
