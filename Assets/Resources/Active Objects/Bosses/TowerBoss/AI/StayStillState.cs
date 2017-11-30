using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayStillState : State
{
    protected bool finished = false;
    protected float time_stopped = 3;
    protected TowerBoss _main_script;
    public override void Enter(Piece piece)
    {
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.CanMove = false;
        _main_script.StartCoroutine(CWaitTime());
    }
    public override void Execute(Piece piece)
    {       
        if (finished)
        {
            if (piece != null)
                _main_script._stateMachine.ChangeState(new WalkAroundState());
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }
    IEnumerator CWaitTime()
    {
        yield return new WaitForSeconds(time_stopped);
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Recover", 0, true, true);

        yield return new WaitForSeconds(1.5f);
        finished = true;
    }

}
