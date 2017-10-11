using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoss : Boss
{
    bool walk;
    GameObject Minion;
    GameObject SpawnEffect;
    void Start()
    {
        walk = true;
        _life = 10; 
        Speed = 60;
        IsInvincible = true;
        base.Start();
        _habilityManager.Hability = new BossDash(this);
        _stateMachine.SetCurrentState(new WalkAroundState());
        _stateMachine.ChangeState( new WalkAroundState());
        _atkArea = Resources.Load<GameObject>("Active Objects/Bosses/TowerBoss/AtkArea/TowerBossAttackArea");
        Minion = Resources.Load<GameObject>("Active Objects/Enemies/Enemy");
        SpawnEffect = Resources.Load<GameObject>("Classes/King/TeleportIn");
    }

    void Update()
    {
    }

    public GameObject CreateMinion(Vector2 pos)
    {
        GameObject toreturn = Instantiate(Minion, transform.position + (Vector3)pos, Quaternion.identity);
        PieceManager.Instance.DecideRandomClass(toreturn.GetComponent<BattlePiece>());
        Instantiate(SpawnEffect, transform.position + (Vector3)pos, Quaternion.identity);
        return toreturn;
    }


}
