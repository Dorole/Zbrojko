using RPG.Core.UI.Dragging;
using UnityEngine;

/// <summary>
/// Place only on destinations for 2D sprites. 
/// This is where a 3D object spawns when the sprite is dragged here.
/// </summary>
public class DropTarget : MonoBehaviour, IDragDestination<SO_ZbrojkoItem>
{
    [SerializeField] private int _maxAcceptable = 10;
    [SerializeField] private ItemType _itemType = ItemType.Number;

    public void AddItems(SO_ZbrojkoItem item, int number)
    {
        //item.SpawnPickup(spawnLocation)
        //spawnLocation bi trebala biti unaprijed odredena, tipa neki grid ili nesto
        //pa se spawnaju na random celiji u gridu
        //napravi class koji prati ukupan zbroj u posudi

        //provjeri ItemType, ne mogu se brojevi spawnati u posudi za Zbrojkice i obrnuto

        Debug.Log("Spawn item");
    }

    public int MaxAcceptable(SO_ZbrojkoItem item)
    {
        return _maxAcceptable;
    }
}
