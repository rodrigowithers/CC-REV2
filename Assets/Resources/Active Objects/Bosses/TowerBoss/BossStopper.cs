using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopper : MonoBehaviour, IStopDash
{
    public int Life = 3;
    public float KnockBack_magnitude = 1;

    public Sprite[] Sprites;

    private SpriteRenderer _sprite;

    // Use this for initialization
    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();

        tag = "B_Stopper";
    }

    public void Hit(GameObject boss)
    {
        boss.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        boss.GetComponent<Rigidbody2D>().velocity = (boss.transform.position - transform.position).normalized * KnockBack_magnitude;

        StartCoroutine(Camera.main.GetComponent<Flash>().CFlash());

        Camera.main.GetComponent<CameraController>().Shake(0.5f);

        Life--;
        CorrectSprite();
    }

    public void CorrectSprite()
    {
        if (Life <= 0)
            Destroy(this.gameObject);
        else
            _sprite.sprite = Sprites[5 - Life];

    }

    //void OnTriggerEnter2D(Collider2D col)
    //{ 
    //    if(col.GetComponent<FakeCannonBall>())
    //    {
    //        Debug.Log("bateu");
    //        Life--;
    //        CorrectSprite();
    //        Destroy(col.gameObject);
    //    }
    //}


}
