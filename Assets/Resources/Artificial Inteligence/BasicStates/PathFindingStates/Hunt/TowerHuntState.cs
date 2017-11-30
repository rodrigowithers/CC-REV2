using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHuntState : HuntState
{
    bool attempt = true;
    float time_between = 3;


    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        //Debug.Log(main_script.AtkAreaDistPlayer());

        //if (main_script.AtkAreaDistPlayer() <= 2)
        //{
        //    main_script._StateMachine.ChangeState(new PrepareAttackState());
        //}

        ///////////////////////////////////////////////////////////////
        ///Regras para que a Torre utilize a sua habilidade especial//
        /////////////////////////////////////////////////////////////
        if (!attempt)
        {
            base.Execute(piece);
            return;
        }

        if (main_script._Hability.HasStamina() && main_script.EnemyDistPlayer() > 5.5f && main_script.EnemyDistPlayer() < 7)
        {    
            main_script._Hability.Use();
            attempt = false;
            main_script.StartCoroutine(CRecuperate());
        }
        else
        {
            base.Execute(piece);
        }
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    IEnumerator CRecuperate()
    {
        yield return new WaitForSeconds(time_between);
        attempt = true;
    }


}
