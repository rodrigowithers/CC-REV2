using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable_Life : Destructable, IKillable
{
    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override IEnumerator HitStun(float time = 0.5F)
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        base.TakeDamage(direction, force, dmg);
    }

    // Use this for initialization
    void Start()
    {
        Life = 1;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}

