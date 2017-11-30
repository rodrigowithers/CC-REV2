using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinionState : State
{
    bool finished = false;
    bool startspawn = false;
    int spawned = 0;
    TowerBoss _main_script;

    int totalSpawn = 3;

    public override void Enter(Piece piece)
    {
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.CanMove = false;

        // Toca a animação
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Idle", 0, true, true); ;

        _main_script.StartCoroutine(CWaitTime());

        totalSpawn = (int)Random.Range(3, 5);
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
                StateChange();
        }
        if (spawned < totalSpawn)
        {
            if (startspawn)
            {
                Vector2 dir = _main_script.RandomDirection();
                _main_script.CreateMinion(dir * 3);
                spawned++; 
                startspawn = false;
                _main_script.StartCoroutine(CWaitTime());
            }
        }
        else
        {
            finished = true;
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }
    IEnumerator CWaitTime()
    {
        yield return new WaitForSeconds(1.5f);
        startspawn = true;
    }


    void StateChange()
    {
        StateMachine SM = _main_script._stateMachine;
        if (SM.GetPreviousState() is ShowerAtkState || SM.GetPreviousState() is SceneShowerAtkState)
        {
            if(_main_script._life < 10)
                SM.ChangeState(new GiantAtkState());
            else
                SM.ChangeState(new ShowerAtkState());
        }
        else if (SM.GetPreviousState() is WalkAroundState)
            SM.ChangeState(new AccurateAtkState());
       
    }
}
