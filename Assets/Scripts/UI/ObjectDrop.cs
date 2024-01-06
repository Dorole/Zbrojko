using UnityEngine;
using UnityEngine.UI;

public class ObjectDrop : MonoBehaviour, IObjectDrop
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemType _itemType;

    private void Start()
    {
        _image.enabled = false;

        DragObject.OnObjectPicked += HandleObjectPicked;
        DragObject.OnObjectDropped += HandleObjectDropped;
    }

    //use tweening for this, see YT videos
    public void HandleObjectPicked(ItemType itemType)
    {
        if (itemType == _itemType)
            _image.enabled = true;
    }

    public void HandleObjectDropped(ItemType itemType)
    {
        if (itemType == _itemType)
            _image.enabled = false;
    }

    private void OnDestroy()
    {
        DragObject.OnObjectPicked -= HandleObjectPicked;
        DragObject.OnObjectDropped -= HandleObjectDropped;
    }

}
