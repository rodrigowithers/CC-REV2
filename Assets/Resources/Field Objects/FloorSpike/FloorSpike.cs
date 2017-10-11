using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpike : MonoBehaviour, IKillable
{
    public int Life = 3;

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator HitStun(float time = 0.5f)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(Vector2 direction, float force = 10)
    {
        Life--;

        if(Life <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IKillable>();
        if(obj != null)
        {
            // Calcula a direção
            var dir = (collision.transform.position - transform.position).normalized;

            obj.TakeDamage(dir);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IKillable>();
        if (obj != null)
        {
            // Calcula a direção
            var dir = (collision.transform.position - transform.position).normalized;

            obj.TakeDamage(dir);
        }
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
