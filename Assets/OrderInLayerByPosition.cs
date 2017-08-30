using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderInLayerByPosition : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public int Precision = 100;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var posy = 1 - Camera.main.WorldToViewportPoint(transform.position).y;

        _spriteRenderer.sortingOrder = (int)(posy * Precision);
    }
}
