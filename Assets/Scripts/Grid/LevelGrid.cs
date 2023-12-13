using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform _gridDebugObjectPrefab = default;
    [SerializeField] private int _gridWidth = 5;
    [SerializeField] private int _gridHeight = 4;
    private GridSystem _gridSystem;
    private int _totalGridPositions;
    private List<GridPosition> _occupiedGridPositions = new List<GridPosition>();
    
    private void Awake()
    {
        _gridSystem = new GridSystem(_gridWidth, _gridHeight, 0.5f, transform);
        _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    private void Start()
    {
        _totalGridPositions = _gridWidth * _gridHeight;       
    }

    public void SetItemAtGridPosition(GridPosition gridPosition, ItemObject item)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        //item.transform.SetParent();
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

    //reset button callback
    public void ClearAllItems()
    {
        foreach (var pos in _occupiedGridPositions)
        {
            ClearItemAtGridPosition(pos);
            
            Transform gridObject = GetGridObjectTransform(pos);
            ItemObject itemObjectComponent = gridObject.GetComponentInChildren<ItemObject>();

            if (itemObjectComponent != null)
                Destroy(itemObjectComponent.gameObject); //implement pooling --> return to pool
            else
                Debug.Log("Item Object component not found.");
        }

        _occupiedGridPositions.Clear();
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    public Transform GetGridObjectTransform(Vector3 worldPosition) => _gridSystem.GetGridObjectTransform(worldPosition);

    public Transform GetGridObjectTransform(GridPosition gridPosition) => _gridSystem.GetGridObjectTransform(gridPosition);

    public bool TryGetUnoccupiedGridPosition(out GridPosition gridPosition)
    {
        if (_occupiedGridPositions.Count >= _totalGridPositions)
        {
            Debug.Log("All grid positions are occupied.");
            gridPosition = new GridPosition();
            return false;
        }

        gridPosition = _gridSystem.GetRandomGridPosition();

        while (_occupiedGridPositions.Contains(gridPosition))
            gridPosition = _gridSystem.GetRandomGridPosition();

        _occupiedGridPositions.Add(gridPosition);
        Debug.Log("Position occupied: " + gridPosition);

        return true;
    }

    private IEnumerator CO_TestCoroutine(float waitTime)
    {
        bool isAllOccupied = false;
        GridPosition gridPos;

        while (!isAllOccupied) 
        { 
            if (!TryGetUnoccupiedGridPosition(out gridPos))
            {
                isAllOccupied = true;
            }

            yield return new WaitForSeconds(waitTime);
        }

        yield return null;
    }
}
