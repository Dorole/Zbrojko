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

    public Path GetRandomPath(ItemType itemType)
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

    public Vector3 GetStartPosition(Path path)
    {
        return path.StartPoint.position;
    }

    public Vector3 GetEndPosition(Path path)
    {
        return path.EndPoint.position;
    }
}
