using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenHuntState : HuntState
{
    bool attempt = true;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        ////////////////////////////////////////////////////////////////
        ///Regras para que o Rainha utilize a sua habilidade especial//
        //////////////////////////////////////////////////////////////

        if (main_script._Hability.HasStamina())
        {
            if(Cornered())
            {
                main_script.Direction = (main_script.transform.position - Camera.main.transform.position).normalized;
                main_script.MovementDirection = (main_script.transform.position - Camera.main.transform.position).normalized;
                main_script._Hability.Use();
                main_script._StateMachine.ChangeState(new FollowPathState());
            }
            else
            {
                base.Execute(piece);
            }
        }
        else
        {
            base.Execute(piece);
        }


        // Regras para após de o peao utilizar habiidade



    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    bool VerifyBorder()
    {
        Vector2 pos = main_script.transform.position;
        if (pos.x > 15 || pos.x < -15)
            return true;
        if (pos.y > 8 || pos.y < -8)
            return true;

        return false;
    }

    bool Cornered()
    {
        if (!VerifyBorder())
            return false;
        else
        {
            if (PlayerIsRanged())
            {
                if (main_script.EnemyDistPlayer() < 3)
                    return true;
            }
            else
            {
                if (main_script.EnemyDistPlayer() < 5 && main_script.EnemyDistPlayer() > 2)
                    return true;
            }
        }
        return false;
    }

}

