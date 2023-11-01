using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSlot
    {
        public int Index;
        public SO_ZbrojkoItem Item;
    }

    public virtual SO_ZbrojkoItem GetItemBySlotIndex(int slotIndex) { return null; }
    protected virtual void AssignIndicesToSlots() { }
}
