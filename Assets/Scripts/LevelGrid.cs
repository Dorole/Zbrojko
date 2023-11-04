using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform _gridDebugObjectPrefab = default;
    private GridSystem _gridSystem;

    private void Awake()
    {
        _gridSystem = new GridSystem(10, 10, 1f);
        _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    public void SetItemAtGridPosition(GridPosition gridPosition, ItemObject item)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.SetItemObject(item);
    }

    public ItemObject GetItemAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetItemObject();
    }

    public void ClearItemAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.SetItemObject(null);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
}
