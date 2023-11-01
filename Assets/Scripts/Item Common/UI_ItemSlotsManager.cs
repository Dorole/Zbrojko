using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemSlotsManager : MonoBehaviour
{
    [SerializeField] private ItemManager _itemManager = default;

    private void Start() 
    {
        //should implement interface later on
        UI_ItemSlot[] slots = GetComponentsInChildren<UI_ItemSlot>();
        //NumbersManager numbersManager = NumbersManager.GetNumbersManager();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Setup(_itemManager, i);
        }
    }
}
