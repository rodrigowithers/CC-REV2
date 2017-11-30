using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : Destructable
{
    public GameObject Stump;

    public override IEnumerator HitStun(float time = 0.5F)
    {
        return base.HitStun(time);
    }

    public override void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        base.TakeDamage(direction, force, dmg);
    }

    public override void Die()
    {
        if(Stump != null)
            Instantiate(Stump, transform.position, Quaternion.identity);

        base.Die();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
