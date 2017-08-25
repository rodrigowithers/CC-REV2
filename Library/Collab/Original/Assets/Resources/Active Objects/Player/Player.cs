using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BattlePiece, IKillable
{
    private PlayerController _controller;
    private HabilityManager _hability;

    private Vector2 _moveInput;

    [Header("Starting Class")]
    public Class Starting;

    #region Damage

    [Header("Damage")]
    
    private bool _invincibleFrames = false;

    public AnimationCurve InvincibilityCurve;

    public int MaxLife = 3;

    public void Die()
    {
        Debug.Log("Player Dies");
    }

    public void TakeDamage(Vector2 direction, float force = 10)
    {
        if (_invincibleFrames || IsInvincible)
            return;

        RigidBody.velocity = direction * force;
        Life--;

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
    public IEnumerator HitStun()
    {
        CanMove = false;
        yield return new WaitForSeconds(0.2f);
        CanMove = true;
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
        _class = (Class)gameObject.AddComponent(typeof(Pawn_Normal));
    }

    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.J))
        {
            PieceManager.Instance.ChangeClass(this, typeof(Pawn_Normal));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            PieceManager.Instance.ChangeClass(this, typeof(Bishop_Normal));
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            PieceManager.Instance.ChangeClass(this, typeof(Tower_Normal));
        }

        // Verifica se usou uma habilidade
        if (_controller.Hability && _hability.isActiveAndEnabled)
        {
            _hability.Use();
        }

        // Pega o Input para mover
        _moveInput = _controller.LAS;

        // Pega o Input de ataque
        _class.Attack(_controller.RAS);
    }

    void FixedUpdate()
    {
        // Move o player
        Move(_moveInput);
    }


}
