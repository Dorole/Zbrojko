using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public event Action<SO_ZbrojkoItem, int> OnGridStateChanged;

    [SerializeField] private ItemType _itemType;
    [SerializeField] private Transform _gridDebugObjectPrefab = default;
    [SerializeField] private int _gridWidth = 5;
    [SerializeField] private int _gridHeight = 4;
    [SerializeField] private ObjectPooler _objectPool;
    [SerializeField] private Vector3 _defaultLocalItemPosition = new Vector3(0, 0.2f, 0);
    
    [SerializeField] private int _occupiedGPCount; // DEBUG

    private GridSystem _gridSystem;
    private int _totalGridPositions;
    private List<GridPosition> _occupiedGridPositions = new List<GridPosition>();

    //********************************** UNITY CALLBACKS **********************************
    private void Awake()
    {
        _gridSystem = new GridSystem(_gridWidth, _gridHeight, 0.5f, transform);
        _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    private void Start()
    {
        _totalGridPositions = _gridWidth * _gridHeight;

        DragObject.OnObjectRemoved += DragObject_OnObjectRemoved;
    }

    private void OnDestroy()
    {
        DragObject.OnObjectRemoved -= DragObject_OnObjectRemoved;
    }

    //********************************** PUBLIC FUNCTIONS **********************************
    public void AddItemToGrid(SO_ZbrojkoItem item)
    {
        TryGetUnoccupiedGridPosition(out GridPosition gridPos);
        SO_GameObjectReference objectRef = item.ItemPrefabReference;
        Transform itemTransform = InstantiateItemInGrid(gridPos, objectRef);
        ItemObject itemObject = ConfigureItemObject(itemTransform, item);

        SetItemAtGridPosition(gridPos, itemObject);

        OnGridStateChanged?.Invoke(item, 1);
    }

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
        _occupiedGPCount = _occupiedGridPositions.Count;

        return true;
    }

    public int GetTotalAvailablePositions()
    {
        return _totalGridPositions;
    }

    public bool HasAvailablePositions()
    {
        return _occupiedGridPositions.Count < _totalGridPositions;
    }

    //reset button callback
    public void ClearAllItems()
    {
        foreach (var pos in _occupiedGridPositions)
        {
            Transform gridObject = GetGridObjectTransform(pos);
            ItemObject itemObjectComponent = gridObject.GetComponentInChildren<ItemObject>();

            if (itemObjectComponent != null)
            {
                SetItemAtGridPosition(pos, null);
                _objectPool.ReturnToPool(itemObjectComponent.GetObjectReference(), itemObjectComponent.gameObject);
            }
            else
                Debug.Log("Item Object component not found.");
        }

        _occupiedGridPositions.Clear();
        _occupiedGPCount = _occupiedGridPositions.Count;
    }

    //********************************** PRIVATE FUNCTIONS **********************************
    private GridObject GetGridObject(GridPosition gridPosition) => 
        _gridSystem.GetGridObject(gridPosition);

    private ItemObject GetItemAtGridPosition(GridPosition gridPosition) =>
    _gridSystem.GetGridObject(gridPosition).GetItemObject();

    private void SetItemAtGridPosition(GridPosition gridPosition, ItemObject item) => 
        _gridSystem.GetGridObject(gridPosition).SetItemObject(item);

    private GridPosition GetItemGridPosition(ItemObject item)
    {
        foreach (var pos in _occupiedGridPositions)
        {
            GridObject gridObject = _gridSystem.GetGridObject(pos);

            if (gridObject.GetItemObject() == item)
                return pos;
        }

        Debug.LogWarning($"Tried to get the position of an item {item.ToString()}, but none was found in grid.");
        return new GridPosition();
    }
    
    private void DragObject_OnObjectRemoved(ItemObject item)
    {
        if (item.GetItemType() != _itemType) return;

        GridPosition itemToRemovePos = GetItemGridPosition(item);
        ClearItemAtGridPosition(itemToRemovePos);

        SO_ZbrojkoItem itemRef = item.GetItemReference();
        OnGridStateChanged?.Invoke(itemRef, -1);
    }

    private void ClearItemAtGridPosition(GridPosition gridPosition)
    {
        SetItemAtGridPosition(gridPosition, null);

        _occupiedGridPositions.Remove(gridPosition);
        _occupiedGPCount = _occupiedGridPositions.Count;
    }

    private Transform InstantiateItemInGrid(GridPosition gridPos, SO_GameObjectReference objectRef)
    {
        Transform itemTransform = _objectPool.SpawnFromPool(objectRef).transform;
        Transform parent = GetGridObjectTransform(gridPos);
        ConfigureInstantiatedTransform(itemTransform, parent);

        return itemTransform;
    }

    private void ConfigureInstantiatedTransform(Transform item, Transform parent)
    {
        item.SetParent(parent);
        item.localPosition = _defaultLocalItemPosition;
        item.localRotation = Quaternion.identity;
        item.gameObject.SetActive(true);
    }

    private ItemObject ConfigureItemObject(Transform itemTransform, SO_ZbrojkoItem item)
    {
        ItemObject itemObject = itemTransform.GetComponent<ItemObject>();
        itemObject.Setup(item);

        return itemObject;
    }

}
