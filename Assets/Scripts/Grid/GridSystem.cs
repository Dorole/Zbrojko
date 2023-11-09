using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem //REFACTOR SO IT TAKES INTO ACCOUNT CONTAINER POSITION
{
    private int _width = default;
    private int _height = default;
    private float _cellSize = default;
    private Transform _parentTransform = default;
    private GridObject[,] _gridObjectArray;
    private List<Transform> _gridObjectTransforms = new List<Transform>(); //scriptable objects?

    public GridSystem(int width, int height, float cellSize, Transform parentTransform)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _parentTransform = parentTransform;

        _gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                //Debug.DrawLine(GetWorldPosition(x,z), GetWorldPosition(x,z) + Vector3.right * .2f, Color.white, 1000);
                
                GridPosition gridPosition = new GridPosition(x, z);
                _gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }        
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / _cellSize),
            Mathf.RoundToInt(worldPosition.z / _cellSize)
        );
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab);
                debugTransform.SetParent(_parentTransform);
                debugTransform.localPosition = GetWorldPosition(gridPosition);
                
                _gridObjectTransforms.Add(debugTransform);

                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return _gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public Transform GetGridObjectTransform(GridPosition gridPosition)
    {
        for (int i = 0; i < _gridObjectTransforms.Count; i++)
        {
            if (_gridObjectTransforms[i].localPosition.x == GetWorldPosition(gridPosition).x
                && _gridObjectTransforms[i].localPosition.z == GetWorldPosition(gridPosition).z)
            {
                return _gridObjectTransforms[i];
            }
        }

        return null;
    }

    public Transform GetGridObjectTransform(Vector3 worldPosition)
    {
        for (int i = 0; i < _gridObjectTransforms.Count; i++)
        {
            if (_gridObjectTransforms[i].localPosition.x == GetGridPosition(worldPosition).x 
                && _gridObjectTransforms[i].localPosition.z == GetGridPosition(worldPosition).z)
            {
                return _gridObjectTransforms[i];
            }    
        }

        return null;
    }

    public GridPosition GetRandomGridPosition()
    {
        int randX = Random.Range(0, _width);
        int randZ = Random.Range(0, _height);

        GridPosition gridPos = _gridObjectArray[randX, randZ].GetGridPosition();
        Debug.Log("Random grid pos: " + gridPos);

        return gridPos;
    }

}