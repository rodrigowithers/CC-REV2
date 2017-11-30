using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BattlePiece, IKillable
{
    private PlayerController _controller;

    private Vector2 _moveInput;

    public AnimationCurve InvincibilityCurve;

    #region Damage

    [Header("Damage")]

    private bool _invincibleFrames = false;
   
    public int MaxLife = 10;

    private bool dead = false;

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    public void Die()
    {
        Debug.Log("Player Dies");

        if(PartyManager.Instance.Classes.Count == 1)
        {
            Death();
        }

        if (PartyManager.Instance.pieceskilled > 1 )
        {
            Death();
        }
        else
        {
            RigidBody.velocity = Vector2.zero;
            Life += MaxLife / 2;            
            PartyManager.Instance.PieceKilled(GetClass.GetType().Name);
            CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ChangePanel;
           
        }
    }
    void Death()
    {
        dead = true;
        // Reseta o EnemyManager
        Destroy(EnemyManager.Instance.gameObject);

        Camera.main.GetComponent<Blackout>().Black();
        GameManager.Instance.Dead = true;
    }

    public void TakeDamage(Vector2 direction, float force = 10, int dmg = 1)
    {
        base.TakeDamage(direction, force,dmg);

        if (_invincibleFrames || IsInvincible)
            return;

        StartCoroutine(CFreezeFrame());

        EnemyManager.Instance.Combo = 0;
        RigidBody.velocity = direction * force;
        Life-=dmg;

        Camera.main.GetComponent<CameraController>().Shake();

        StartCoroutine(Camera.main.GetComponent<Flash>().CFlash());

        StartCoroutine(HitStun());
        StartCoroutine(CIFrames());
    }

    public IEnumerator CIFrames()
    {
        _invincibleFrames = true;

        float time = 0.0f;

        var oldColor = _renderer.color;

        while (time < 1.0f)
        {
            oldColor = _renderer.color;
            oldColor.a = InvincibilityCurve.Evaluate(time / InvincibilityCurve.GetCurveLenght());

            _renderer.color = oldColor;

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        oldColor.a = 1;
        _renderer.color = oldColor;

        yield return new WaitForSeconds(0.5f);

        _invincibleFrames = false;
        yield return null;
    }

    #endregion


    private void Awake()
    {
        base.Awake();
       
        _controller = GetComponent<PlayerController>();
        _hability = GetComponent<HabilityManager>();

        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        base.Start();

        Type classe = Type.GetType(PartyManager.Instance.Classes[0].Name);

        if (classe != null)
        {
            _class = (Class)gameObject.AddComponent(classe);
        }
        else
        {
            _class = (Class)gameObject.AddComponent(typeof(Pawn_Normal));
        }

        GameManager.Instance._Player = this.gameObject;

        if(GameManager.Instance.Mode == GameManager.GameMode.HARD)
        {
            Debug.Log("Hard Mode");
            Life = MaxLife = 3;
        }

    }

    void Update()
    {
        if (dead)
            return;

        base.Update();

        if(Life > MaxLife)
        {
            Life = MaxLife;
        }

        // Verifica se usou uma habilidade
        if (_controller.Hability && _hability.isActiveAndEnabled)
        {
            _hability.Use();
        }

        // Pega o Input para mover
        _moveInput = _controller.LAS;

        // Pega o Input de ataque

        if (Stamina > AttackCost)
        {
            _class.Attack(_controller.RAS);
        }

        // Verifica se morreu
        if(Life <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (dead)
            return;
        // Move o player
        Move(_moveInput);
    }
}
