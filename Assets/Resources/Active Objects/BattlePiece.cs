using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePiece : Piece, IAttacker
{
    protected Class _class;
    protected HabilityManager _hability;

    public int Life = 2;

    public float Stamina = 100;
    public float StaminaRegen = 20;

    public void SetHability(string h)
    {
        //switch (h)
        //{
        //    case "Dash":
        //        _hability.Hability = new Dash(this);
        //        break;
        //    case "RopeDash":
        //        _hability.Hability = new RopeDash(this);
        //        break;
        //    case "SpeedUp":
        //        _hability.Hability = new SpeedUp(this);
        //        break;
        //    case "Teleport":
        //        _hability.Hability = new Teleport(this);
        //        break;
        //    case "WallDash":
        //        _hability.Hability = new WallDash(this);
        //        break;
        //    case "InvincibleDash":
        //        _hability.Hability = new InvincibleDash(this);
        //        break;
        //    case "DamageDash":
        //        _hability.Hability = new DamageDash(this);
        //        break;
        //}
    }

    public float AttackCost = 20;

    protected bool _canRegen = true;
    private Coroutine _regening;

    public bool CanRegen
    {
        get
        {
            return _canRegen;
        }
        set
        {
            _canRegen = value;
            if (!_canRegen)
            {
                if (_regening != null)
                    StopCoroutine(_regening);

                _regening = StartCoroutine(RegenTime());
            }
        }
    }

    private IEnumerator RegenTime()
    {
        yield return new WaitForSeconds(_class.StaminaRegenTimer);
        //yield return new WaitForSeconds(0.5f);
        _canRegen = true;
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public Class GetClass
    {
        get
        {
            return _class;
        }
        set
        {
            _class = value;
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

    public IEnumerator CRecoverAttack()
    {
        throw new NotImplementedException();
    }
}
