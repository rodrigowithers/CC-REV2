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
        float checkdist = 0.5f;


        if (main_script.AtkAreaDistPlayer() <= checkdist)
        {
            main_script._StateMachine.ChangeState(new PrepareAttackState());
        }
        EnemyManager.Instance.AtkAreaUpdate(main_script.gameObject);

        unit.StopAllCoroutines();
        unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position - (Vector3)main_script.AtkAreaToEnemy()));

        if (main_script.EnemyDistPlayer() > 6)
        {
            main_script._StateMachine.ChangeState(new FollowPathState());
        }
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }
  
}
