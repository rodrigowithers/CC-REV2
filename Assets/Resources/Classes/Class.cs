using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    public GameObject AttackArea;
    protected GameObject _attackArea;

    public float  atkareacheck = 0.5f;

    private string _hability;
    public virtual string Hability
    {
        get
        {
            return _hability;
        }
    }

    public CHESSPIECE Type;
    private bool _canAttack = false;
    public bool CanAttack
    {
        get { return _canAttack; }
        set
        {
            _canAttack = value;

            if (_canAttack == false)
            {
                CurrentCooldown = 0.0f;
            }
        }
    }

    public virtual float MovementSpeed
    {
        get
        {
            return 300;
        }
    }
    public virtual float AttackSpeed
    {
        get
        {
            return 0.5f;
        }
        set
        {

        }
    }
    public virtual float AttackDuration
    {
        get
        {
            return 0.1f;
        }
    }

    public virtual float StaminaRegenTimer
    {
        get
        {
            return 0.5f;
        }
    }

    public float CurrentCooldown;

    protected Transform[] _attackAreas;

    public virtual void Attack(Vector2 direction)
    {
        // Pega as AttackAreas
        if (_attackArea == null)
            return;

        _attackAreas = _attackArea.transform.GetChildrenOfType(typeof(AttackArea));   
        //_attackAreas = transform.GetChild(0).GetChildrenOfType(typeof(AttackArea));
    }

    public virtual void Start()
    {
        // Pega a Piece a qual essa classe foi colocada
        Piece p = GetComponent<Piece>();
        if (p != null)
        {
            // Seta as variaveis de acordo com a classe
            p.Speed = MovementSpeed;
        }

        // Pega as AttackAreas
        _attackAreas = _attackArea.transform.GetChildrenOfType(typeof(AttackArea));

        if (_attackArea == null || _attackAreas == null)
            Destroy(this.gameObject);

        //_attackAreas = transform.GetChild(0).GetChildrenOfType(typeof(AttackArea));
        foreach (var area in _attackAreas)
        {
            // Seta as variaves de acordo com a classe
            area.GetComponent<AttackArea>().Duration = AttackDuration;
        }


        CurrentCooldown = 0.0f;
    }

    public void Update()
    {
        if (CurrentCooldown < AttackSpeed)
        {
            CurrentCooldown += Time.deltaTime;

            if (CurrentCooldown >= AttackSpeed)
            {
                CanAttack = true;
            }
        }
    }

    public virtual void Hunt_Close()
    {

    }
    public virtual void Hunt_Ranged()
    {

    }
    public virtual void Hunt_Close_Smart()
    {

    }
    public virtual void Hunt_Ranged_Smart()
    {

    }
}
