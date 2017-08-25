using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private bool _selected = false;
    private SpriteRenderer _renderer;

    private bool _attacking = false;

    private float _duration;

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

            // Troca as cores
            if (!_selected)
            {
                _renderer.color = new Color(0, 0, 0);
            }
            else
            {
                _renderer.color = new Color(1, 1, 1);
            }
        }
    }

    public virtual bool LockPosition
    {
        get
        {
            return true;
        }
    }

    public IEnumerator CAttack()
    {
        _attacking = true;

        var oldParent = transform.parent;
        var oldPos = transform.localPosition;

        if (LockPosition)
        {
            transform.parent = null;
        }

        var oldColor = _renderer.color;

        // Muda a cor
        _renderer.color = new Color(0, 255, 255);

        yield return new WaitForSeconds(_duration);

        _attacking = false;
        _renderer.color = oldColor;
        if (LockPosition)
        {
            transform.parent = oldParent;
            transform.localPosition = oldPos;
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_attacking)
            return;

        if (collision.GetComponent(typeof(IKillable)))
        {
            IKillable obj = collision.GetComponent<IKillable>();
            obj.Die();
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Selected = false;
    }

    void Update()
    {

    }
}
