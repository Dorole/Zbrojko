using RPG.Core.UI.Dragging;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Place only on destinations for 2D sprites. 
/// This is where a 3D object spawns when the sprite is dragged here.
/// </summary>
public class DropTarget : MonoBehaviour, IDragDestination<SO_ZbrojkoItem>
{
    [SerializeField] private ItemType _acceptableItemType = ItemType.Number;
    [SerializeField] private ObjectPooler _objectPool;
    [SerializeField] private int _maxAcceptable = 10;
    [SerializeField] private LevelGrid _levelGrid;
    [SerializeField] private Vector3 _defaultLocalItemPosition = new Vector3(0, 0.2f, 0);

    public event Action<SO_ZbrojkoItem> OnItemAdded;

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        //MathTeacher class prati ukupan zbroj u posudi - trebat ce ovdje i Remove funkcija

        if (_acceptableItemType != item.ItemType)
        {
            Debug.Log("Item types don't match.");
            return;
        }

        _levelGrid.TryGetUnoccupiedGridPosition(out GridPosition gridPos);

        SO_GameObjectReference objectRef = item.ItemPrefabReference;

        Transform itemTransform = InstantiateItemInGrid(gridPos, objectRef);
        ItemObject itemObject = ConfigureItemObject(itemTransform, item);
        _levelGrid.SetItemAtGridPosition(gridPos, itemObject);

        OnItemAdded?.Invoke(item);
    }

    public int MaxAcceptable(SO_ZbrojkoItem item)
    {
        //make use of this!
        return _maxAcceptable;
    }

    private Transform InstantiateItemInGrid(GridPosition gridPos, SO_GameObjectReference objectRef)
    {
        Transform itemTransform = _objectPool.SpawnFromPool(objectRef).transform; 
        Transform parent = _levelGrid.GetGridObjectTransform(gridPos);
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
