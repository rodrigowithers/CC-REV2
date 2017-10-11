using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDrop : MonoBehaviour, IKillable
{
    public int Life = 1;

    public GameObject[] Drops;

    public void Die()
    {
        int itens = UnityEngine.Random.Range(1, 5);

        for (int i = 0; i < itens; i++)
        {
            Instantiate(Drops[UnityEngine.Random.Range(0, Drops.Length)], transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    public IEnumerator HitStun(float time = 0.5f)
    {
        yield return null;
    }

    public void TakeDamage(Vector2 direction, float force = 5)
    {
        Life -= 1;

        if(Life <= 0)
        {
            Die();
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
