using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelGrid _levelGrid;
    [SerializeField] private Transform _itemPrefab;

    private void Start()
    {
        GridPosition gridPos = new GridPosition(2, 3); //randomize

        //Object - world related/visual
        Transform _itemTransform = Instantiate(_itemPrefab);
        Transform parent = _levelGrid.GetGridObjectTransform(gridPos);

        _itemTransform.SetParent(parent);
        _itemTransform.localPosition = new Vector3(0, 0.2f, 0); //hardcoded

        //Object - grid related
        ItemObject itemObject = _itemTransform.GetComponent<ItemObject>();
        _levelGrid.SetItemAtGridPosition(gridPos, itemObject);
    }
}
