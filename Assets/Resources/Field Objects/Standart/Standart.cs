using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standart : MonoBehaviour
{
    public GameObject Explosion;

    public Sprite King;
    private SpriteRenderer _sprite;

    public void ChangeToKing()
    {
        if (Explosion != null)
            Instantiate(Explosion, transform.position, Quaternion.identity);

        _sprite.sprite = King;
    }

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }
}
