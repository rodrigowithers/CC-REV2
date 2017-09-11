using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KingHuntState : HuntState
{
    bool attempt = true;

    
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    
    public override void Execute(Piece piece)
    {

        ///////////////////////////////////////////////////////////////
        ///Regras para que o Rei utilize a sua habilidade especial////
        /////////////////////////////////////////////////////////////

        if (EnemyManager.Instance.EnemyCount == 1)
        {

            if (main_script._Hability.HasStamina())
            {
                if (main_script.EnemyDistPlayer() > 8.0f )
                {
                    main_script._Hability.Use();
                }
            }
            else
            {
                base.Execute(piece);
            }
        }


        // Regras para após de o peao utilizar habiidade



    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }



}