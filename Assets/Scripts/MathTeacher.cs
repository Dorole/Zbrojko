using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MathTeacher : MonoBehaviour
{
    [SerializeField] private DropTarget[] _dropTargets;
    [Header("DEBUG")]
    [SerializeField] private int _zbrojkiciSum = 0;
    [SerializeField] private int _numbersSum = 0;

    public event Action OnCalculationEqual;

    private void Start()
    {
        foreach (var dropTarget in _dropTargets) 
        {
            dropTarget.OnItemAdded += DropTarget_OnItemAdded;
        }
    }

    private void DropTarget_OnItemAdded(SO_ZbrojkoItem item)
    {
        switch (item.ItemType)
        {
            case ItemType.Zbrojkic:
                _zbrojkiciSum += item.Value;
                break;
            case ItemType.Number:
                _numbersSum += item.Value;
                break;
            default:
                break;
        }

        if (_zbrojkiciSum == _numbersSum)
            OnCalculationEqual?.Invoke();

    }

    private void OnDestroy()
    {
        foreach (var dropTarget in _dropTargets)
        {
            dropTarget.OnItemAdded -= DropTarget_OnItemAdded;
        }
    }

    //reset button callback
    public void ResetScore()
    {
        _zbrojkiciSum = 0;
        _numbersSum = 0;
    }
}
