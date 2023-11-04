using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text = default;
    private GridObject _gridObject;
    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
    }

    private void Update()
    {
        _text.text = _gridObject.ToString();
    }
}
