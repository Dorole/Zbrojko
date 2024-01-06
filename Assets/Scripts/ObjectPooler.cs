using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> _pools;
    private Dictionary<SO_GameObjectReference, Queue<GameObject>> _poolDictionary;

    // ****************************************************** UNITY CALLBACKS ******************************************************
    private void Start()
    {
        _poolDictionary = new Dictionary<SO_GameObjectReference, Queue<GameObject>>();
        FillPools();
    }

    // ****************************************************** PRIVATE FUNCTIONS ******************************************************   
    private void FillPools()
    {
        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject o = InstantiateItem(pool);
                objectQueue.Enqueue(o);
            }

            _poolDictionary.Add(pool.GameObjectReference, objectQueue);
        }
    }

    /// <summary>
    /// Expand pool if all items from the pool are out and more are needed.
    /// </summary>
    private void ExpandPool(SO_GameObjectReference objectRef)
    {
        Pool pool = _pools.Find(item => item.GameObjectReference == objectRef);

        if (pool == null)
        {
            Debug.LogWarning("Trying to expand non-existing pool of type " + objectRef.ToString());
            return;
        }

        pool.Size++;
        GameObject o = InstantiateItem(pool);
        _poolDictionary[objectRef].Enqueue(o);
    }

    private GameObject InstantiateItem(Pool pool)
    {
        GameObject o = Instantiate(pool.GameObjectReference.ReferencedObject);
        o.transform.SetParent(pool.Parent);
        o.SetActive(false);

        return o;
    }

    private void ResetObject(SO_GameObjectReference objectRef, GameObject objectToReset)
    {
        Pool pool = _pools.Find(item => item.GameObjectReference == objectRef);
        objectToReset.transform.SetParent(pool.Parent, false);
        objectToReset.transform.position = pool.Parent.position; 
        objectToReset.transform.rotation = Quaternion.identity;
    }

    // ****************************************************** PUBLIC FUNCTIONS ******************************************************
    public GameObject SpawnFromPool(SO_GameObjectReference objectRef)
    {
        if (!_poolDictionary.ContainsKey(objectRef))
        {
            Debug.LogWarning("Pool of type " + objectRef.ToString() + " doesn't exist!");
            return null;
        }

        if (_poolDictionary[objectRef].Count <= 0)
            ExpandPool(objectRef);

        GameObject o = _poolDictionary[objectRef].Dequeue();

        return o;
    }

    public void ReturnToPool(SO_GameObjectReference objectRef, GameObject objectToReturn)
    {
        _poolDictionary[objectRef].Enqueue(objectToReturn);
        objectToReturn.SetActive(false);

        ResetObject(objectRef, objectToReturn);
    }
}
