using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), (typeof(ParticleSystem)))]
public class ParticleCopySprite : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mat = _particleSystem.GetComponent<ParticleSystemRenderer>().material;
        mat.mainTexture = _spriteRenderer.sprite.texture;
    }
}
