using UnityEngine;

public class SO_ZbrojkoItem : ScriptableObject
{
    [SerializeField] private GameObject _prefab; //TEMP > make ScriptableReference
    [SerializeField] private ItemType _itemType = default;
    [SerializeField] private int _value;
    [SerializeField] Sprite _icon = default; //potencijalno Zbrojkici ne? Kako s animacijom?

    public GameObject ItemPrefab => _prefab;
    public ItemType ItemType => _itemType;
    public int Value => _value;
    public Sprite Icon => _icon;

    public void SpawnPickup()
    {
        //potencijalno vrati nesto, GameObject ili idk
        //prefab bi trebao imati referencu na SO zbog ItemType
    }
}