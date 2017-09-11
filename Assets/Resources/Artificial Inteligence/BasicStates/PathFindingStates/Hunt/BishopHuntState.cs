using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopHuntState : HuntState
{
    bool attempt = true;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
          //////////////////////////////////////////////////////////////
         ///Regras para que a Bispa utilize a sua habilidade especial//
        //////////////////////////////////////////////////////////////

        if (main_script._Hability.HasStamina())
        {
            if (main_script.EnemyDistPlayer() < 5.0f && main_script.EnemyDistPlayer() > 3.0f)
            {
                main_script._Hability.Use();
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



}
