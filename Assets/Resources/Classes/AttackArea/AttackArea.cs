﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    protected Piece _parent;

    protected bool _selected = false;
    protected SpriteRenderer _renderer;

    protected bool _attacking = false;

    private float _duration;

    //private LineRenderer Line;

    private List<GameObject> _insideCollider = new List<GameObject>();

    public virtual float Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            _duration = value;
        }
    }

    public bool Selected
    {
        get
        {
            return _selected;
        }

        set
        {
            _selected = value;

            if (_attacking)
                return;
        }
    }

    public virtual bool LockPosition
    {
        get
        {
            return true;
        }
    }

    public virtual IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;

        _attacking = true;

        yield return new WaitForSeconds(0.1f);

        var oldParent = transform.parent;
        var oldPos = transform.localPosition;

        List<GameObject> insideCollider = new List<GameObject>(_insideCollider);

        if (LockPosition)
        {
            transform.parent = null;
        }

        float time = 0.0f;

        bool hitted = false;

        // Fica testando, esperando dar dano
        while (time < _duration)
        {
            time += Time.deltaTime;


            // Pega o tamanho da sprite
            var size = _renderer.sprite.bounds.extents.x;

            if (insideCollider.Count > 0 && !hitted)
            {
                foreach (var obj in insideCollider)
                {
                    if (obj != null)
                    {
                        // Calcula a direção do objeto à area de ataque
                        var dir = (obj.transform.position.xy() - transform.position.xy()).normalized;

                        // Checa se vai matar o inimigo, se for matar, restaura a stamina de quem está atacando
                        if (obj.GetComponent<BattlePiece>() && obj.GetComponent<BattlePiece>().Life == 1)
                        {
                            _parent.GetComponent<BattlePiece>().Stamina = 100;
                        }

                        obj.GetComponent<IKillable>().TakeDamage(dir);

                        hitted = true;
                        break;
                    }
                }
            }

            yield return null;
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IKillable>();
        if (obj != null && !_insideCollider.Contains(collision.gameObject))
        {
            _insideCollider.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        var obj = collision.GetComponent<IKillable>();
        if (obj != null && _insideCollider.Contains(collision.gameObject))
        {
            _insideCollider.Remove(collision.gameObject);
        }
    }

    private void Awake()
    {
        //Line = GetComponent<LineRenderer>();


        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Selected = false;
        _parent = transform.parent.parent.GetComponent<Piece>();

        if (_parent.GetComponent<Player>() != null)
        {
            _renderer.color = Color.white;
        }
        else
        {
            _renderer.color = Color.red;
        }

        //Line.startColor = Line.endColor = _renderer.color;
    }

    void Update()
    {
        //Line.SetPositions(new Vector3[] {
        //    transform.position,
        //    _parent.transform.position
        //});
    }
}
