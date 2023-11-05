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
        //Object - world related/visual
        Transform _itemTransform = Instantiate(_itemPrefab);
        Transform parent = _levelGrid.GetGridObjectTransform(_positionToInstantiate);
        _itemTransform.SetParent(parent);
        _itemTransform.localPosition = new Vector3(0, 0.2f, 0); //hardcoded

        //Object - grid related
        ItemObject itemObject = _itemTransform.GetComponent<ItemObject>();
        GridPosition gridPosition = _levelGrid.GetGridPosition(_positionToInstantiate);
        _levelGrid.SetItemAtGridPosition(gridPosition, itemObject);
    }

}
