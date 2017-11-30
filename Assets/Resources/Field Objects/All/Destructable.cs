using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IKillable
{
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual IEnumerator HitStun(float time = 0.5F)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeDamage(Vector2 direction, float force = 5, int dmg = 1)
    {
        Life -= dmg;

        // Spawna particulas
        Instantiate(_particles, transform.position, Quaternion.identity);

        // Toca um som
        SoundManager.Play("pedra lascando");

        if (Life <= 0)
            Die();
    }

    protected GameObject _particles;
    public int Life = 1;

    // Use this for initialization
    public void Start()
    {
        _particles = Resources.Load<GameObject>("Field Objects/HitParticles");
    }

    // Update is called once per frame
    public void Update()
    {

    }
}
