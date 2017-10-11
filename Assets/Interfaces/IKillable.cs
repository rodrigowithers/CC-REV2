using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    IEnumerator HitStun(float time = 0.5f);

    void TakeDamage(Vector2 direction, float force = 5);
    void Die();
}
