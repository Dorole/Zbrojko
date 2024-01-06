using UnityEngine;

public class SO_ZbrojkoItem : ScriptableObject
{
    [SerializeField] private SO_GameObjectReference _prefabReference; 
    [SerializeField] private ItemType _itemType = default;
    [SerializeField] private int _value;
    [SerializeField] Sprite _icon = default; //potencijalno Zbrojkici ne? Kako s animacijom?

    //public GameObject ItemPrefab => _prefabReference.ReferencedObject;
    public SO_GameObjectReference ItemPrefabReference => _prefabReference;
    public ItemType ItemType => _itemType;
    public int Value => _value;
    public Sprite Icon => _icon;

}