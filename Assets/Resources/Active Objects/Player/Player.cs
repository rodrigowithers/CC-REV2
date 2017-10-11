﻿using System;
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

        // Reseta o EnemyManager
        Destroy(EnemyManager.Instance.gameObject);

        // Pega a cena atual
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    public void TakeDamage(Vector2 direction, float force = 10)
    {
        base.TakeDamage(direction, force);

        if (_invincibleFrames || IsInvincible)
            return;

        StartCoroutine(CFreezeFrame());

        EnemyManager.Instance.Combo = 0;
        RigidBody.velocity = direction * force;
        Life--;

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
        Type classe = Type.GetType(PartyManager.Instance.Classes[0]);

        if (classe != null)
        {
            _class = (Class)gameObject.AddComponent(classe);
        }
        else
        {
            _class = (Class)gameObject.AddComponent(typeof(Pawn_Normal));
        }

    }

    void Update()
    {
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
        // Move o player
        Move(_moveInput);
    }
}
