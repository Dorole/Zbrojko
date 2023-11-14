using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelGrid _levelGrid;
    [SerializeField] private Transform _itemPrefab;

    private void Start()
    {
        
    }

    private void Update()
    {


    }

    public void Test_InstantiateObjectAtRandomPos()
    {
        _levelGrid.TryGetUnoccupiedGridPosition(out GridPosition gridPos);

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
