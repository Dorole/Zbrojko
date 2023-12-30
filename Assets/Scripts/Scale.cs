using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scale : MonoBehaviour
{
    [SerializeField] private DropTarget[] _dropTargets;
    [SerializeField] private float _time = 1;
    [SerializeField] private float _xDiff = 10; //TEST ONLY, should use anim curve or multiply value from SO
    private Quaternion _defaultRotation;

    void Start()
    {
        _defaultRotation = transform.rotation;

        foreach (var dropTarget in _dropTargets)
        {
            dropTarget.OnItemAdded += DropTarget_OnItemAdded;
        }
    }

    private void DropTarget_OnItemAdded(SO_ZbrojkoItem item)
    {
        float xRot = transform.eulerAngles.x;
        float targetX = xRot;

        switch (item.ItemType)
        {
            case ItemType.Zbrojkic:
                targetX = xRot - item.Value;
                break;
            case ItemType.Number:
                targetX = xRot + item.Value;
                break;
            default:
                break;
        }

        LeanTween.rotateX(gameObject, targetX, _time).setEaseOutBack();

    }

    private void OnDestroy()
    {
        foreach (var dropTarget in _dropTargets)
        {
            dropTarget.OnItemAdded -= DropTarget_OnItemAdded;
        }
    }

    //reset button callback
    public void ResetScale()
    {
        transform.rotation = _defaultRotation;
    }
}
