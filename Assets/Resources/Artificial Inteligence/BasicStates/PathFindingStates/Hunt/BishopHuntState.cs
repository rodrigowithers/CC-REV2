using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopHuntState : HuntState
{
    bool attempt = true;
    float timebetweenspecials = 5.0f;


    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        //////////////////////////////////////////////////////////////
        ///Regras para que a Bispa utilize a sua habilidade especial//
        //////////////////////////////////////////////////////////////
        if (attempt)
        {
            if (main_script._Hability.HasStamina())
            {
                if (main_script.EnemyDistPlayer() > 3.5f && main_script.EnemyDistPlayer() < 6.0f)
                {
                    if (EnemyManager.Instance.EnemyCount > 1)
                    {
                        main_script._Hability.Use();
                        attempt = false;
                        main_script.StartCoroutine(CRecover());
                    }
                }
            }
        }
        base.Execute(piece);

    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    IEnumerator CRecover()
    {
        yield return new WaitForSeconds(timebetweenspecials);
        attempt = true;
        yield return null;
    }


}
