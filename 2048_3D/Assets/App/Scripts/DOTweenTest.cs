using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DOTweenTest : MonoBehaviour
{
    [SerializeField] private Material _cubeMaterial;
    [SerializeField] private TMP_Text _indexOfCube;

    private int index = 2;
    private void Start()
    {
        transform.DOMove(new Vector3(0, 5, 0), 1.5f).From().SetLoops(-1, LoopType.Yoyo);
    }

    private void OnValueChange()
    {
        _indexOfCube.text = (index*2).ToString();
    }
}
