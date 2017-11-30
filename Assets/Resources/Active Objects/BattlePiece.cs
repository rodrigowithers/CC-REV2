using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePiece : Piece, IAttacker, IKillable
{
    protected Class _class;
    protected HabilityManager _hability;
    public int Life = 2;
    public CHESSPIECE _Type;
    public float Stamina = 100;
    public float StaminaRegen = 20;

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

   

    public void Die()
    {

    }

    public void Pushback(Vector2 dir, float force = 10)
    {
        if (IsInvincible)
            return;

        //faz com que o corpo pare de se mover
        RigidBody.velocity = Vector2.zero;


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

    public void hitStun(float time = 0.5f)
    {
        StartCoroutine(HitStun(time));    
    }

    public IEnumerator CFreezeFrame(int frames = 5)
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.01f;
            yield return new WaitForSecondsRealtime(0.016f * frames); // 2 frame
            Time.timeScale = 1.0f;
        }

        yield return null;
    }

    public IEnumerator HitStun(float time = 0.5f)
    {
        CanMove = false;
        yield return new WaitForSeconds(time);
        CanMove = true;
    }

    private IEnumerator RegenTime()
    {
        yield return new WaitForSeconds(_class.StaminaRegenTimer);
        //yield return new WaitForSeconds(0.5f);
        _canRegen = true;
    }

    public AnimationCurve DamageCurve;
    public GameObject HitParticles;

    public void TakeDamage(Vector2 direction, float force = 10, int dmg = 1)
    {
        // Toca um som
        SoundManager.Play("hit");

        // Spawna particulas
        Instantiate(HitParticles, transform.position, Quaternion.identity);
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
    public void Start()
    {
        // Carrega particulas
        HitParticles = Resources.Load<GameObject>("Field Objects/HitParticles");
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
