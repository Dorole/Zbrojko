using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        public Transform StartPoint;
        public Transform EndPoint;
    }

    [SerializeField] private List<Path> _numberPaths = new List<Path>();
    [SerializeField] private List<Path> _zbrojkicPaths = new List<Path>();

    private Path GetRandomPath(ItemType itemType)
    {
        if (itemType == ItemType.Number)
        {
            if (_numberPaths.Count == 0)
            {
                Debug.LogWarning("Number path list not set!");
                return new Path();
            }

            int randIndex = Random.Range(0, _numberPaths.Count);
            return _numberPaths[randIndex];
        }
        else
        {
            if (_zbrojkicPaths.Count == 0)
            {
                Debug.LogWarning("Zbrojkic path list not set!");
                return new Path();
            }

            int randIndex = Random.Range(0, _zbrojkicPaths.Count);
            return _zbrojkicPaths[randIndex];
        }
    }

    private Vector3 GetStartPosition(Path path)
    {
        if (path.StartPoint == null)
        {
            Debug.LogError("StartPoint is null in GetStartPosition!");
            return Vector3.zero; 
        }

        return path.StartPoint.position;
    }

    private Vector3 GetEndPosition(Path path)
    {
        if (path.EndPoint == null)
        {
            Debug.LogError("EndPoint is null in GetEndPosition!");
            return Vector3.zero;
        }

        return path.EndPoint.position;
    }

    public (Vector3 start, Vector3 end) GetRandomPathPositions(ItemType itemType)
    {
        Path path = GetRandomPath(itemType);

        return (GetStartPosition(path), GetEndPosition(path));
    }
}
