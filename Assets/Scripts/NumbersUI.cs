using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersUI : MonoBehaviour
{
    [SerializeField] private NumbersManager _numbersManager = default;

    private void Awake() 
    {
        //should implement interface later on
        UI_NumberSlot[] slots = GetComponentsInChildren<UI_NumberSlot>();
        //NumbersManager numbersManager = NumbersManager.GetNumbersManager();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Setup(_numbersManager, i);
        }
    }
}
