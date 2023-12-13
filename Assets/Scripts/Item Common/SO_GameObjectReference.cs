using UnityEngine;

[CreateAssetMenu(menuName = ("Zbrojko/Game Object Reference"))]
public class SO_GameObjectReference : ScriptableObject
{
    [SerializeField] private GameObject _gameObject = default;

    public GameObject ReferencedObject => _gameObject;
}
