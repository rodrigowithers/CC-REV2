using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseHuntState : HuntState
{
    bool attempt = true;
    bool failed = false;
    bool foundpath = false;
    bool gotClose = false;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        Debug.Log("HorseHuntState");
    }

    public override void Execute(Piece piece)
    {

          ////////////////////////////////////////////////////////////////
         ///Regras para que o Cavalo utilize a sua habilidade especial///
        ////////////////////////////////////////////////////////////////

        if(attempt) // Aims at player and try to get close to him
        {
            if (!gotClose)
            {
                if (main_script.EnemyDistPlayer() < 4)
                {
                    gotClose = false;
                }
            }
            else
            {
                // In case the player gets relatively far
                // the horse opts for falling back for a little while and do it over
                if (main_script.EnemyDistPlayer() > 4)
                {
                    attempt = false;
                }
            }

            if (main_script._Hability.HasStamina())
            {
                main_script._Hability.Use();
            }

            base.Execute(piece);


        }
        else
        {
            if(!foundpath)
            {
                unit.RequestNewPathTo(new Vector2(-1000,0)); // random place
                foundpath = true;
            }
            if(main_script._Hability.HasStamina())
                main_script._Hability.Use();

            if (unit.FinishedPath())
            {
                attempt = true;
                foundpath = false;
            }
        }






        // Regras para após de o peao utilizar habiidade



    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }



}