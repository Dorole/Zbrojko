using RPG.Core.UI.Dragging;
using System;
using UnityEngine;

/// <summary>
/// Place only on destinations for 2D sprites. 
/// This is where a 3D object spawns when the sprite is dragged here.
/// </summary>
public class DropTarget : MonoBehaviour, IDragDestination<SO_ZbrojkoItem>
{
    [SerializeField] private int _maxAcceptable = 10;
    [SerializeField] private ItemType _itemType = ItemType.Number; //rename to _acceptableItemType
    [SerializeField] private Transform _parentObject = default;
    [SerializeField] private Vector3 _instantiateAtPosition; //TEST

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        //item.SpawnPickup(spawnLocation)
        //spawnLocation bi trebala biti unaprijed odredena, tipa neki grid ili nesto
        //pa se spawnaju na random celiji u gridu
        //MathTeacher class prati ukupan zbroj u posudi - trebat ce ovdje i Remove funkcija

        if (_itemType != item.ItemType)
        {
            Debug.Log("Item types don't match.");
            return;
        } 

        Debug.Log("Spawn item");
        //refactor to pool
        GameObject o = Instantiate(item.ItemPrefab, _instantiateAtPosition, Quaternion.identity);
        o.transform.parent = _parentObject;
        o.GetComponent<ItemObject>().Setup(item); 
    }

    public int MaxAcceptable(SO_ZbrojkoItem item)
    {
        return _maxAcceptable;
    }
}
