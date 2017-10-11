using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HarpoonShooter : MonoBehaviour, IKillable
{
    public bool _active = true;
    public void Active()
    {
        _active = !_active;

        _lineRenderer.enabled = _active;
    }

    public float Life = 3;
    public GameObject Harpoon;

    public float Distance = 10;
    public Vector2 Direction;

    private LineRenderer _lineRenderer;

    public float AttackCooldown = 1.0f;
    private float _currentCooldown = 0;

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator HitStun(float time = 0.5f)
    {
        yield return null;
    }

    public void TakeDamage(Vector2 direction, float force = 10)
    {
        Life--;

        if(Life <= 0)
        {
            Die();
        }
    }

    private void Shoot()
    {
        _currentCooldown = 0;

        // Instancia um espinho
        var obj = Instantiate(Harpoon, transform.position, Quaternion.identity);

        obj.GetComponent<Harpoon>().Direction = Direction;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_active)
            return;

        // Casta um raio para frente, esperando algo passar
        var hits = Physics2D.RaycastAll(transform.position.xy(), Direction.normalized, Distance);

        foreach(var hit in hits)
        {
            if (hit.collider.GetComponent<BattlePiece>())
            {
                if(_currentCooldown >= AttackCooldown)
                {
                    Shoot();
                }
            }
        }

        if(_currentCooldown < AttackCooldown)
        {
            _currentCooldown += Time.deltaTime;
        }

        // Atualiza o Line Renderer
        _lineRenderer.SetPositions(new Vector3[]{
            transform.position,
            transform.position.xy() + (Direction.normalized * Distance)
        });
    }
}
