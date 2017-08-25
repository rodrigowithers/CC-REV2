using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{

    //GameObject _player_ref;
    public override void Enter(Piece piece)
    {
        base.Enter(piece);

        main_script.SetColor(Color.red);
        main_script.Use_Path = true;

    }
    public override void Execute(Piece piece)
    {
        //main_script.Direction = main_script.AtkAreaDirPlayer();
        //main_script.Move(main_script.Direction);
        //main_script.Move();
        
        if (main_script.AtkAreaDistPlayer() < 0.5f)
        {
            main_script._StateMachine.ChangeState(new PrepareAttackState());
        }

        unit.StopAllCoroutines();
        unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position - (Vector3)main_script.AtkAreaToEnemy()));
        if (main_script.EnemyDistPlayer() > 6)
        {
            // player ja chegou perto 
            // muda de estado 
            main_script._StateMachine.ChangeState(new FollowPathState());
        }
        //CheckforDanger();

        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    //protected bool CanUseHability()
    //{
    //    //Verifica se a peça possui stamina o suficiente para utilizar a habilidade
    //    if (main_script._Hability.Hability.Cost <= main_script._Hability.Stamina)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
  
}
