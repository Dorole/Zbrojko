using System;
using System.Collections;
using UnityEngine;

public class ItemObjectMover : MonoBehaviour
{
    [Header("Move time")]
    [SerializeField] private float _dropTime = 1f;
    [SerializeField] private float _moveTime = 3f;
    [Header("Move positions")]
    [SerializeField] private float _minY = -1;
    [SerializeField] private float _maxY = 1f;
    [SerializeField] private float _xMoveDiff = 5f; 

    private ObjectPooler _pool;
    private ItemObject _item;
    private DragObject _dragObject;

    public event Action OnObjectDropped;

    // ************************************ UNITY CALLBACKS ************************************
    private void Awake()
    {
        _item = GetComponent<ItemObject>();
        _dragObject = GetComponent<DragObject>();
    }

    private void OnEnable()
    {
        if (_pool == null)
            _pool = FindObjectOfType<ObjectPooler>();
        
        _dragObject.OnObjectRemoved += DragObject_OnObjectRemoved;
        
    }

    private void OnDisable()
    {
        _dragObject.OnObjectRemoved -= DragObject_OnObjectRemoved;
    }

    // ************************************ PUBLIC FUNCTIONS ************************************
    //animation event
    public void MoveOut()
    {
        float moveDirection = (_item.GetItemType() == ItemType.Zbrojkic) ? _xMoveDiff * -1 : _xMoveDiff;

        LeanTween.moveX(gameObject, transform.position.x + moveDirection, _moveTime)
                .setEaseLinear()
                .setOnComplete(() => OnMoveComplete());
    }

    //animation event
    public void RotateLeft()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
    }

    // ************************************ PRIVATE FUNCTIONS ************************************
    private void DragObject_OnObjectRemoved()
    {
        StartCoroutine(CO_DropObject());
    }

    private IEnumerator CO_DropObject()
    {
        float newY = UnityEngine.Random.Range(_minY, _maxY);
        LeanTween.moveY(gameObject, newY, _dropTime);

        yield return new WaitForSeconds(_dropTime);
        OnObjectDropped?.Invoke();
    }

    private void OnMoveComplete()
    {
        _pool.ReturnToPool(_item.GetObjectReference(), gameObject);
    }


}
