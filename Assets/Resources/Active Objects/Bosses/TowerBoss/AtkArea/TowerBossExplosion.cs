using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBossExplosion : MonoBehaviour
{
    public float Scale = 1;
    public int damage = 1;
    void Start()
    {
        // Casta um circulo ao redor, para dar dano
        var hits = Physics2D.CircleCastAll(transform.position, 2 * Scale, Vector2.zero);
        foreach(var hit in hits)
        {
            var obj = hit.collider.GetComponent<IKillable>();

            // Desconsidera dano no próprio Boss
            if (obj != null && hit.collider.GetComponent<TowerBoss>() == null)
            {
                Vector2 dir = (hit.transform.position - transform.position).normalized;

                obj.TakeDamage(dir, 10,damage);
            }
        }
    }
}
