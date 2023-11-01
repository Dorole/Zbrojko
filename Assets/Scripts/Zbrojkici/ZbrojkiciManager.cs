using UnityEngine;

public class ZbrojkiciManager : ItemManager
{
    //[SerializeField] private SO_Zbrojkic[] _zbrojkicScriptableObjects = default;
    [SerializeField] private SO_Zbrojkic _defaultZbrojkicItem = default;
    [SerializeField] private ItemSlot[] _zbrojkicSlots = default;

    public override SO_ZbrojkoItem GetItemBySlotIndex(int index)
    {
        return _zbrojkicSlots[index].Item;
    }

    public SO_Zbrojkic GetDefaultZbrojkicItem()
    {
        return _defaultZbrojkicItem;
    }

    private void Awake()
    {
        AssignIndicesToSlots();
        AssignIdenticalItemToAllSlots();
    }

    protected override void AssignIndicesToSlots()
    {
        for (int i = 0; i < _zbrojkicSlots.Length; i++)
        {
            _zbrojkicSlots[i].Index = i;
        }
    }

    private void AssignIdenticalItemToAllSlots()
    {
        for (int i = 0; i < _zbrojkicSlots.Length; i++)
        {
            _zbrojkicSlots[i].Item = _defaultZbrojkicItem;
        }
    }

    //private void AssignRandomZbrojkicItem() {}
}
