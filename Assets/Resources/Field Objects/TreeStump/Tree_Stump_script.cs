using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tree_Stump_script : MonoBehaviour , IKillable {

    public int life = 1;
    public Sprite[] sprites;
    SpriteRenderer sr;
    public void Die()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator HitStun(float time = 0.5F)
    {
        throw new NotImplementedException();
    }
    public void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        life--;
        if (life <= 0)
            Die();
        else
            sr.sprite = sprites[life - 1];
    }

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = sprites[life-1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
