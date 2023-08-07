using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderInLayerByPosition : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private GameObject _pivot;

    public int Precision = -1;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //if(_pivot == null)
        //{
        //    _pivot = GameObject.Find("OrderInLayerPivot");

        //    if(_pivot == null)
        //    {
        //        GameObject go = new GameObject("OrderInLayerPivot");
        //        _pivot = go;
        //    }
        //}
    }

    void Start()
    {

    }

    void Update()
    {
        //var posy = 1 - _pivot.transform.position.y;
        var posy = transform.position.y;

        _spriteRenderer.sortingOrder = (int)(posy * Precision);
    }
}
