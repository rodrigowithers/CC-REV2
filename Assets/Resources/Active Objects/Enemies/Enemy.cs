using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StateMachine))]
public class Enemy : BattlePiece, IKillable
{
    #region Variables
    public string Name;
    public LEVEL Level;
    protected StateMachine statemachine;
    public bool Use_Path = true;
    public bool Smart = false;
    public bool IsAttacking = false;
    #endregion

    public GameObject Drop;

    public GameObject DeathExplosion;

    public float dropChance = 0.5f;

    public UnityEvent DieEvent;

    public AnimationCurve InvincibilityCurve;

    public StateMachine _StateMachine
    {
        get
        {
            return statemachine;
        }
    }
    public LEVEL _Level
    {
        get
        {
            return Level;
        }
        set
        {
            Level = value;
        }
    }
    public HabilityManager _Hability
    {
        get
        {
            return _hability;
        }
        set
        {
            _hability = value;
        }
    }

    public Transform CurrentAtkArea
    {
        get
        {
            return transform.GetChild(0).GetComponent<AttackPattern>().CurrenAtkPattern;
        }
        set
        {
            transform.GetChild(0).GetComponent<AttackPattern>().CurrenAtkPattern = value;
        }
    }

    public virtual void Die()
    {
        if (DeathExplosion != null)
            Instantiate(DeathExplosion, transform.position, Quaternion.identity);

        // Retorna stamina ao jogador
        Player.Instance.GetComponent<Player>().Stamina = 100;

        // Invoca o evento
        DieEvent.Invoke();

        // Dropa
        if(Drop != null) {
            if(UnityEngine.Random.Range(0.0f, 1.0f) > dropChance){
                Instantiate(Drop, transform.position, Quaternion.identity);
            }
        }

        EnemyManager.Instance.RemoveEnemy(this.gameObject);
        Destroy(this.gameObject);
    }

    public void TakeDamage(Vector2 direction, float force = 10,int dmg = 1)
    {
        if (IsInvincible)
            return;

        base.TakeDamage(direction, force, dmg);

        Life -= dmg;

        if(Life <= 0)
        {
            // Checa se é o ultimo inimigo, se for, da zoom nele
            if (EnemyManager.Instance.EnemyCount == 1)
                Camera.main.GetComponent<CameraController>().Zoom(transform.position.xy());
        }

        StartCoroutine(CDamageFlash());

        CanMove = false;
        StartCoroutine(HitStun());

        if (Life <= 0)
        {
            Die();
            return;
        }
        else
        {
            StartCoroutine(CFreezeFrame());
        }

        // Empurra o corpo para traz
        if(RigidBody != null)
            RigidBody.velocity = direction * force;
    }

    public void Start()
    {
        base.Start();

        Speed -= 20;

        NewDirection();
        _hability = GetComponent<HabilityManager>();

        if(Smart)
        {
            Material smartmat = Resources.Load<Material>("Active Objects/Enemies/EnemySmartMaterial") as Material;
            GetComponent<SpriteRenderer>().material = smartmat;
        }
        
    }

    void Update()
    {
        base.Update();
    }

    //fução virtual, recebe override de todos os filhos para que estes possam 
    // por si mesmos dizer como um lesser,normal,greater enemy se comporta
    public virtual void VerifyLevel()
    {
    }

    public bool CheckNearWalls()
    {
        Vector3 cam = Camera.main.transform.position;
        Vector3 pos = transform.position;
        if (pos.x >= 16f || pos.x <= -16f)
            return true;
        if (pos.y >= cam.y + 8 || pos.y <= cam.y - 9)
            return true;
        return false;
    }


    void WasHit()
    {
        if (statemachine.GetCurrentState().GetType() == typeof(WanderPathState))
        {
            statemachine.ChangeState(new AwareState());
        }
        else if (statemachine.GetCurrentState().GetType() == typeof(WanderPathState))
        {

        }
    }


    #region Helpers
    public Vector2 EnemyDirPlayer()
    {
        GameObject p = GameManager.Instance._Player;
        Vector2 dir;
        dir = (p.transform.position - transform.position);
        dir.Normalize();
        return dir;
    }
    public float EnemyDistPlayer()
    {
        GameObject p = GameManager.Instance._Player;
        Vector2 dist;
        dist = (p.transform.position - transform.position);
        return dist.magnitude;
    }
    public Vector2 AtkAreaToEnemy()
    {
        if (CurrentAtkArea == null)
            return Vector2.zero;

        Vector2 v = CurrentAtkArea.position - transform.position;
        return v;
    }
    public Vector2 AtkAreaDirPlayer()
    {
        if (CurrentAtkArea == null)
            return Vector2.zero;

        GameObject p = GameManager.Instance._Player;
        Vector2 dir;

        dir = (p.transform.position - CurrentAtkArea.position);
        dir.Normalize();
        return dir;
    }
    public float AtkAreaDistPlayer()
    {
        if (CurrentAtkArea == null)
            return 0;

        GameObject p = GameManager.Instance._Player;
        Vector2 dist;
        dist = (p.transform.position - CurrentAtkArea.position);
        return dist.magnitude;
    }
    public Vector2 AtkAreaToPlayer()
    {
        return AtkAreaDirPlayer() * AtkAreaDistPlayer();
    }

    //Retorna o angulo entre o inimigo e o player
    public float ThisAngleFromPlayer()
    {
        if (Player.Instance == null)
            return 0;

        GameObject p = GameManager.Instance._Player;


        Vector2 normal = transform.position - p.transform.position;
        return Vector2.Dot(new Vector2(1, 0), normal.normalized);
    }
    //Retorna o angulo entre a area de ataque atual e o player
    public float AtkAreaAngleFromPlayer()
    {
        if (Player.Instance == null)
            return 0;

        GameObject p = GameManager.Instance._Player;
        if (p == null)
            return 0;

        Vector2 normal = CurrentAtkArea.position - p.transform.position;
        return Vector2.Dot(new Vector2(1, 0), normal.normalized);
    }
    //Retorna o angulo entre um GameObject e o player
    public float ObjectAngleFromPlayer(GameObject go)
    {
        if (Player.Instance == null)
            return 0;

        GameObject p = GameManager.Instance._Player;
        if (p == null)
            return 0;

        Vector2 normal = go.transform.position - p.transform.position;
        return Vector2.Dot(new Vector2(1, 0), normal.normalized);
    }

    //Retorna o angulo entre um GameObject e o player
    public float Pos_Enemy_Dist(Vector2 pos)
    {
        return (pos - (Vector2)transform.position).magnitude;
    }
    public Vector2 Pos_Enemy_Dir(Vector2 pos)
    {
        Vector2 dir = pos - (Vector2)transform.position;
        return dir.normalized;
    }
    public float Pos_Enemy_Angle(Vector2 pos)
    {
        Vector2 normal = (Vector2)transform.position - pos;
        return Vector2.Dot(new Vector2(1, 0), normal.normalized);
    }


    //// será usada pelas classes filhas de follow
    protected bool PlayerIsRanged()
    {
        CHESSPIECE T = Player.Instance.GetComponent<Player>().GetClass.Type;
        if (T == CHESSPIECE.QUEEN || T == CHESSPIECE.TOWER || T == CHESSPIECE.BISHOP)
            return true;

        return false;
    }

    #endregion
}
