using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccurateAtkState : State
{
    protected bool finished = false;
    protected bool CanAtk = false;
    protected int atktimes = 3;
    protected GameObject area = null;
    protected TowerBoss _main_script;
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.CanMove = false;
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
                StateChange();
        }
        if (atktimes > 0)
        {
            if (area == null)
            {
                atktimes--;
                area = CreateAtkArea(player.transform.position);
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
    protected GameObject CreateAtkArea(Vector2 pos)
    {
        GameObject toreturn = GameObject.Instantiate(_main_script._atkArea);
        toreturn.transform.position = pos;
        return toreturn;
    }


    void StateChange()
    {
        StateMachine SM = _main_script._stateMachine;
        if (SM.GetPreviousState() is WalkAroundState )
            SM.ChangeState(new WalkAroundState());
        else if ( (SM.GetPreviousState() is SpawnMinionState || 
                    SM.GetPreviousState() is AccurateAtkState))
        {
            if(EnemyManager.Instance.HasEnemies)
                SM.ChangeState(new AccurateAtkState());
            else
                SM.ChangeState(new ShowerAtkState());
        }

    }

}
