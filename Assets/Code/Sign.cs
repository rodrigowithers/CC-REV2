using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sign : Destructable, IKillable
{
    public UnityEvent OnDestroyEvent;
    
    public override void Die()
    {
        OnDestroyEvent.Invoke();
        Destroy(this.gameObject);
    }

    public IEnumerator HitStun(float time = 0.5F)
    {
        yield return null;
    }

    public override void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        base.TakeDamage(direction, force, dmg);
    }

    private void OnDestroy()
    {

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
