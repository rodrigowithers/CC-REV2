using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFollow : FollowState
{
    protected bool isranged = false;
    protected bool canusehability = true;
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        //isranged = 
        base.Execute(piece);

        if (main_script.EnemyDistPlayer() > 12)
        {
            UseDash();
        }

    }
    public override void Exit(Piece piece)
    {
        //base.Execute(piece);
    }

    protected void UseDash()
    {
        main_script._Hability.Use();
        main_script.StartCoroutine(CWait());
        canusehability = false;
    }


    IEnumerator CWait()
    {
        yield return new WaitForSeconds(3);
        canusehability = true;
    }
}


