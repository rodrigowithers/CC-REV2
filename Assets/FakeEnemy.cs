using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : Enemy, IKillable
{
    public void Die()
    {

    }

    public IEnumerator HitStun(float time = 0.5F)
    {
        yield return null;
    }

    public void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
