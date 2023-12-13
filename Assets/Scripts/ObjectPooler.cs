using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> _pools;
    private Dictionary<ItemType, Queue<GameObject>> _poolDictionary;

    // ****************************************************** UNITY CALLBACKS ******************************************************
    private void Start()
    {
        _poolDictionary = new Dictionary<ItemType, Queue<GameObject>>();
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

            _poolDictionary.Add(pool.ItemType, objectQueue);
        }
    }

    /// <summary>
    /// Expand pool if all items from the pool are out and more are needed.
    /// </summary>
    private void ExpandPool(ItemType itemType)
    {
        Pool pool = _pools.Find(item => item.ItemType == itemType);

        if (pool == null)
        {
            Debug.LogWarning("Trying to expand non-existing pool of type " + itemType.ToString());
            return;
        }

        pool.Size++;
        GameObject o = InstantiateItem(pool);
        _poolDictionary[itemType].Enqueue(o);
    }

    private GameObject InstantiateItem(Pool pool)
    {
        GameObject o = Instantiate(pool.GameObjectReference.ReferencedObject);
        o.transform.SetParent(pool.Parent);
        o.SetActive(false);

        return o;
    }

    private void ResetObject(ItemType itemType, GameObject objectToReset)
    {
        Pool pool = _pools.Find(item => item.ItemType == itemType);
        objectToReset.transform.SetParent(pool.Parent, false);
        objectToReset.transform.position = pool.Parent.position;
        objectToReset.transform.rotation = Quaternion.identity;
    }

    // ****************************************************** PUBLIC FUNCTIONS ******************************************************
    public GameObject SpawnFromPool(ItemType itemType)
    {
        if (!_poolDictionary.ContainsKey(itemType))
        {
            Debug.LogWarning("Pool of type " + itemType.ToString() + " doesn't exist!");
            return null;
        }

        if (_poolDictionary[itemType].Count <= 0)
            ExpandPool(itemType);

        GameObject o = _poolDictionary[itemType].Dequeue();

        return o;
    }

    public void ReturnToPool(ItemType itemType, GameObject objectToReturn)
    {
        _poolDictionary[itemType].Enqueue(objectToReturn);
        objectToReturn.SetActive(false);

        ResetObject(itemType, objectToReturn);
    }
}
