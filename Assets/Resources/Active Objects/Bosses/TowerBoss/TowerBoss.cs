using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoss : Boss
{
    bool walk;
    GameObject Minion;
    GameObject SpawnEffect;
    public GameObject Canonball;
    public GameObject FakeCannonBall;

    public ParticleSystem Particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IAttack>() != null && IsInvincible)
        {
            SoundManager.Play("clank");
        }
    }

    void Start()
    {
        walk = true;
        _life = 20;
        Speed = 60;
        IsInvincible = true;
        base.Start();
        _habilityManager.Hability = new BossDash(this);

        // Carrega o Animator
        gameObject.AddComponent<ClassAnimator>();
        gameObject.GetComponent<ClassAnimator>().LoadAnimations("Active Objects/Bosses/TowerBoss/Animations/TowerBossAnimationController");

        // Desativa as Partículas
        var emission = Particles.emission;
        emission.enabled = false;

        _atkArea = Resources.Load<GameObject>("Active Objects/Bosses/TowerBoss/AtkArea/TowerBossAttackArea");
        Minion = Resources.Load<GameObject>("Active Objects/Enemies/Enemy");
        SpawnEffect = Resources.Load<GameObject>("Classes/King/TeleportIn");
        Canonball = Resources.Load<GameObject>("Classes/Tower/Cannon/CannonBall");
        _stateMachine.SetCurrentState(new WalkAroundState());
        _stateMachine.ChangeState(new WalkAroundState());
    }

    void Update()
    {
        base.Update();

        if (IsInvincible && !Particles.emission.enabled)
        {
            var emission = Particles.emission;
            emission.enabled = true;
        }

        if (!IsInvincible && Particles.emission.enabled)
        {
            var emission = Particles.emission;
            emission.enabled = false;
        }
    }

    public void ShakeScreen()
    {
        if(SoundManager.Instance != null)
        {
            // Som
            SoundManager.Play("rumbling_short");
        }

        Camera.main.GetComponent<CameraController>().Shake();
    }

    public GameObject CreateMinion(Vector2 pos)
    {
        bool[] seed = { false, false, false, false, false, true };
        GameObject toreturn = Instantiate(Minion, transform.position + (Vector3)pos, Quaternion.identity);
        PieceManager.Instance.DecidewithSeed(toreturn.GetComponent<BattlePiece>(), seed);
        Instantiate(SpawnEffect, transform.position + (Vector3)pos, Quaternion.identity);

        toreturn.GetComponent<Enemy>().Life = 2;
        toreturn.GetComponent<Enemy>().dropChance = 0.8f;

        SoundManager.Play("spawn");

        EnemyManager.Instance.AddEnemy(toreturn);

        return toreturn;
    }
}
