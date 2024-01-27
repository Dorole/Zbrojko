using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Material[] _materials;
    [SerializeField] private bool _changeMaterialOnEnable = true;

    private void OnEnable()
    {
        if (_changeMaterialOnEnable)
            SetNewMaterial();
    }

    private void SetNewMaterial()
    {
        if (_materials.Length < 1) return;

        int newMaterialIndex;

        do
        {
            newMaterialIndex = Random.Range(0, _materials.Length);
        }
        while (_materials[newMaterialIndex] == _meshRenderer.material);

        _meshRenderer.material = _materials[newMaterialIndex];
    }
}
