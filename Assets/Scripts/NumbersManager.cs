using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NumbersManager : MonoBehaviour
{
    [SerializeField]
    NumberSlot[] _numberSlots = default;

    [System.Serializable]
    public struct NumberSlot
    {
        public int Index;
        public SO_Number NumberItem;
    }

    public SO_Number GetNumberItem(int index)
    {
        return _numberSlots[index].NumberItem;
    }

    private void OnValidate() //switch to OnEnable or Awake
    {
        AssignIndicesToSlots();
    }

    private void AssignIndicesToSlots()
    {
        for (int i = 0; i < _numberSlots.Length; i++)
        {
            _numberSlots[i].Index = i;
        }
    }
}
