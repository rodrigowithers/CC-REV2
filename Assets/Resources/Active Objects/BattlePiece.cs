using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePiece : Piece, IAttacker, IKillable
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

    public void TakeDamage(Vector2 direction, float force = 10)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }

    public void Pushback(Vector2 dir, float force = 10)
    {
        if (IsInvincible)
            return;

        // Empurra o corpo para traz
        RigidBody.velocity = dir * force;
        StartCoroutine(PushBackStun());
    }

    private IEnumerator PushBackStun()
    {
        CanMove = false;
        yield return new WaitForSeconds(0.1f);
        CanMove = true;
    }

    public IEnumerator HitStun()
    {
        CanMove = false;
        yield return new WaitForSeconds(0.5f);
        CanMove = true;
    }

    private IEnumerator RegenTime()
    {
        yield return new WaitForSeconds(_class.StaminaRegenTimer);
        //yield return new WaitForSeconds(0.5f);
        _canRegen = true;
    }

    public AnimationCurve DamageCurve;


    // Alterna entre a cor da sprite e branco
    public IEnumerator CDamageFlash()
    {
        float time = 0.0f;

        var oldColor = _renderer.color;

        while (time < 1.0f)
        {
            var color = _renderer.color;

            var percentage = DamageCurve.Evaluate(time);

            var newR = Mathf.Lerp(color.r, 1.0f, percentage);
            var newG = Mathf.Lerp(color.g, 1.0f, percentage);
            var newB = Mathf.Lerp(color.b, 1.0f, percentage);

            _renderer.color = new Color(newR, newG, newB, 1);

            time += Time.deltaTime * 5.0f;
            yield return null;
        }

        _renderer.color = oldColor;

        yield return null;
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
    protected void Update()
    {
        if (Stamina < 100 && CanRegen)
            Stamina += Time.deltaTime * StaminaRegen;
    }

    public IEnumerator CRecoverAttack()
    {
        throw new NotImplementedException();
    }

}
