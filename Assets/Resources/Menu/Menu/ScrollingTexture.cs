using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTexture : MonoBehaviour
{
    //private Material _material;
    private RawImage _img;

    // Use this for initialization
    void Start()
    {
        //var spriteRenderer = GetComponent<SpriteRenderer>();
        //_material = spriteRenderer.material;

        _img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        //_material.mainTextureOffset += Vector2.one * Time.deltaTime * 0.5f;

        var pos = _img.uvRect.position;
        pos += Vector2.one * Time.deltaTime * 0.5f;

        if (pos.Equals(Vector2.one))
            pos = Vector2.zero;


        _img.uvRect = new Rect(pos, _img.uvRect.size);
    }
}
