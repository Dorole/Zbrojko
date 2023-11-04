using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelGrid _levelGrid;
    [SerializeField] private Transform _itemPrefab;
    [SerializeField] private Vector3 _positionToInstantiate = new Vector3(1, 0, 2);


    private void Start()
    {
        Transform _itemTransform = Instantiate(_itemPrefab, _positionToInstantiate, Quaternion.identity);
        ItemObject itemObject = _itemTransform.GetComponent<ItemObject>();

        GridPosition gridPosition = _levelGrid.GetGridPosition(_itemTransform.position);
        _levelGrid.SetItemAtGridPosition(gridPosition, itemObject);
    }


}
