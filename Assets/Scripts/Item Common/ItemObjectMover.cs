using System.Collections;
using UnityEngine;

public class ItemObjectMover : MonoBehaviour
{
    [Header("Move time")]
    [SerializeField] private float _dropTime = 1f;
    [SerializeField] private float _moveTime = 2f;
    [Header("Move positions")]
    [SerializeField] private float _minY = -1;
    [SerializeField] private float _maxY = 1f;
    [SerializeField] private float _xMoveDiff = 5f; 

    private ObjectPooler _pool;
    private ItemObject _item;

    private void Awake()
    {
        _item = GetComponent<ItemObject>();
    }

    private void OnEnable()
    {
        if (_pool == null)
            _pool = FindObjectOfType<ObjectPooler>();
        
        DragObject.s_OnObjectRemoved += DragObject_OnObjectRemoved;
    }

    private void DragObject_OnObjectRemoved(ItemObject item)
    {
        if (item.GetItemType() != _item.GetItemType() || item != _item)
            return;

        StartCoroutine(CO_MoveObject());
    }

    private IEnumerator CO_MoveObject()
    {
        float newY = Random.Range(_minY, _maxY);
        LeanTween.moveY(gameObject, newY, _dropTime);

        yield return new WaitForSeconds(_dropTime);

        float moveDirection = (_item.GetItemType() == ItemType.Zbrojkic) ? _xMoveDiff * -1 : _xMoveDiff;

        LeanTween.moveX(gameObject, transform.position.x + moveDirection, _moveTime)
                .setEaseLinear()
                .setOnComplete(() => OnMoveComplete());
    }

    private void OnMoveComplete()
    {
        _pool.ReturnToPool(_item.GetObjectReference(), gameObject);
    }

    private void OnDisable()
    {
        DragObject.s_OnObjectRemoved -= DragObject_OnObjectRemoved;
    }
}
