using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDrop : Destructable, IKillable
{
    public GameObject[] Drops;

    public override void Die()
    {
        int itens = UnityEngine.Random.Range(1, 5);
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            for (int i = 0; i < itens; i++)
            {
                Instantiate(Drops[UnityEngine.Random.Range(0, Drops.Length)], transform.position, Quaternion.identity);
            }
        }

        Destroy(this.gameObject);
    }

    public override IEnumerator HitStun(float time = 0.5f)
    {
        yield return null;
    }

    public override void TakeDamage(Vector2 direction, float force = 5,int dmg = 1)
    {
        base.TakeDamage(direction, force, dmg);
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
