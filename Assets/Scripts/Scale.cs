using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scale : MonoBehaviour
{
    [SerializeField] private LevelGrid[] _levelGrids;
    [SerializeField] private float _time = 1;
    [SerializeField] private float _xDiff = 10; //TEST ONLY, should use anim curve or multiply value from SO
    private Quaternion _defaultRotation;

    void Start()
    {
        _defaultRotation = transform.rotation;

        foreach (var levelGrid in _levelGrids)
        {
            levelGrid.OnGridStateChanged += LevelGrid_OnGridStateChanged;
        }
    }

    private void LevelGrid_OnGridStateChanged(SO_ZbrojkoItem item, int operation)
    {
        float xRot = transform.eulerAngles.x;
        float targetX = xRot;

        switch (item.ItemType)
        {
            case ItemType.Zbrojkic:
                targetX = xRot - (item.Value * operation);
                break;
            case ItemType.Number:
                targetX = xRot + (item.Value * operation);
                break;
            default:
                break;
        }

        LeanTween.rotateX(gameObject, targetX, _time).setEaseOutBack();

    }

    private void OnDestroy()
    {
        foreach (var levelGrid in _levelGrids)
        {
            levelGrid.OnGridStateChanged -= LevelGrid_OnGridStateChanged;
        }
    }

    //reset button callback
    public void ResetScale()
    {
        transform.rotation = _defaultRotation;
    }
}
