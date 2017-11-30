using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnHuntState : HuntState
{
    bool attempt = true;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        /////////////////////////////////////////////////////////////
        ///Regras para que o peão utilize a sua habilidade especial//
        /////////////////////////////////////////////////////////////
        //base.Execute(piece);

        //Debug.Log(main_script.EnemyDistPlayer());

        if (main_script.AtkAreaDistPlayer() < 1.0f)
        {
            main_script._StateMachine.ChangeState(new PrepareAttackState());
        }

        if (main_script._Hability.HasStamina())
        {
            if (main_script.EnemyDistPlayer() < 5.0f && main_script.EnemyDistPlayer() > 4.0f)
            {
                main_script._Hability.Use();
            }
        }

        if(main_script.Stamina > 10)
        {
            unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position - (Vector3)main_script.AtkAreaToEnemy()));
        }
        else
        {
            unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position + (Vector3)main_script.AtkAreaToEnemy() * 2));
        }



        //unit.StopAllCoroutines();
        //if (main_script.EnemyDistPlayer() > 6)
        //{
        //    // player ja chegou perto 
        //    // muda de estado 
        //    main_script._StateMachine.ChangeState(new FollowPathState());
        //}

        //base.Execute(piece);



        // Regras para após de o peao utilizar habiidade

    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }



}
