using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour, IAttack
{
    public float Speed = 2;
    public Vector2 Destination;

    protected Vector2 _originalPosition;
    public Vector2 Direction;

    protected float time = 0;

    protected bool _damage = true;

    public GameObject LanceParticles;

    public BattlePiece Piece;

    // Use this for initialization
    void Start()
    {
        //Direction = Destination - transform.position.xy();
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, Direction);
    }

    // Update is called once per frame
    void Update()
    {
        // Casta um raio para frente
        Debug.DrawRay(transform.position, Direction/3, Color.cyan);
        var hits = Physics2D.RaycastAll(transform.position, Direction/3, 1);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<IStopDash>() != null)
            {
                Destroy(this.gameObject);
            }

            var obj = hit.collider.GetComponent<IKillable>();
            if (obj != null && _damage && hit.collider.GetComponent<BattlePiece>() != Piece)
            {
                obj.TakeDamage(Direction);
                _damage = false;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition += Direction.xyz(transform.localPosition) * Time.deltaTime * Speed;

        time += Time.deltaTime * Speed;        
        if (time >= 0.5f)
        {
            LanceParticles.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
