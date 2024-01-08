using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEditor.Progress;

public class MathTeacher : MonoBehaviour
{
    [SerializeField] private LevelGrid[] _levelGrids;
    [Header("DEBUG")]
    [SerializeField] private int _zbrojkiciSum = 0;
    [SerializeField] private int _numbersSum = 0;

    public event Action OnCalculationEqual;

    private void Start()
    {
        foreach (var levelGrid in _levelGrids)
        {
            levelGrid.OnGridStateChanged += LevelGrid_OnGridStateChanged;
        }
    }

    //operation (1 or -1) - addition or deduction
    private void LevelGrid_OnGridStateChanged(SO_ZbrojkoItem item, int operation)
    {
        switch (item.ItemType)
        {
            case ItemType.Zbrojkic:
                _zbrojkiciSum += (item.Value * operation);
                break;
            case ItemType.Number:
                _numbersSum += (item.Value * operation);
                break;
            default:
                break;
        }

        if (_zbrojkiciSum == _numbersSum && _zbrojkiciSum != 0)
            OnCalculationEqual?.Invoke();
    }

    private void OnDestroy()
    {
        foreach (var levelGrid in _levelGrids)
        {
            levelGrid.OnGridStateChanged -= LevelGrid_OnGridStateChanged;
        }
    }

    //reset button callback
    public void ResetScore()
    {
        _zbrojkiciSum = 0;
        _numbersSum = 0;
    }
}
