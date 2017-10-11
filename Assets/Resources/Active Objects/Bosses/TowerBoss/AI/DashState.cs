using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    bool finished = false;
    bool dashed = false;
    TowerBoss _main_script;
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.StartCoroutine(CWaitToDash());
    }
    public override void Execute(Piece piece)
    {
        if(finished)
        {
            if (dashed && _main_script.CanMove)
            {
                Debug.Log("AcabouDash");
                if (!_main_script.IsInvincible)
                {
                    _main_script._stateMachine.ChangeState(new RecoverState());
                }
                else
                {
                    _main_script._stateMachine.ChangeState(new WalkAroundState());
                }
            }
            if (!dashed)
            {
                //Utiliza o BossDash, este por si alterará o estado do Boss
                //dependendo se ele colidiu com algo durante o Dash
                _main_script.MovementDirection = DirToPlayer();
                _main_script._habilityManager.Use();
                dashed = true;
            }            
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
        Debug.Log("Exited DashState");
    }

    //Roda a animação que prepara o boss para o dash
    IEnumerator CWaitToDash()
    {
        yield return new WaitForSeconds(3);
        finished = true;
    }

    Vector2 DirToPlayer()
    {
        return (player.transform.position - _main_script.transform.position).normalized;
    }

}
