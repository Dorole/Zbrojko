using System.Collections;
using UnityEngine;

public class ItemObjectMover : MonoBehaviour
{
    [Header("Move time")]
    [SerializeField] private float _dropTime = 1f;
    [SerializeField] private float _moveTime = 2f;
    [Header("Move positions")]
    [SerializeField] private float _minY = 1.8f;
    [SerializeField] private float _maxY = 3.5f;
    [SerializeField] private float _xMoveDiff = 5f; 

    //private Waypoints _waypoints;
    private ObjectPooler _pool;
    private ItemObject _item;

    private void OnEnable()
    {
        //if ( _waypoints == null)
        //    _waypoints = FindObjectOfType<Waypoints>();

        if (_pool == null)
            _pool = FindObjectOfType<ObjectPooler>();
        
        DragObject.OnObjectRemoved += DragObject_OnObjectRemoved;
    }

    private void Awake()
    {
        _item = GetComponent<ItemObject>();
    }

    private void DragObject_OnObjectRemoved(ItemObject item)
    {
        if (item.GetItemType() != _item.GetItemType() || item != _item) 
            return;

        StartCoroutine(CO_MoveObject());
    }

    private IEnumerator CO_MoveObject()
    {
        //var positions = _waypoints.GetRandomPathPositions(_item.GetItemType());

        float newY = Random.Range(_minY, _maxY);
        LeanTween.moveY(gameObject, newY, _dropTime).setEaseOutBounce();
        
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
        DragObject.OnObjectRemoved -= DragObject_OnObjectRemoved;
    }
}
