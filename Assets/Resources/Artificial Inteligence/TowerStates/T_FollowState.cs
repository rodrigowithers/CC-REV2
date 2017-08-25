using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_FollowState : FollowState
{
    
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
    }
    public override void Execute(Piece piece)
    {
        base.Execute(piece);
        if(main_script.EnemyDistPlayer() > 4)
        {
            main_script._Hability.Use();
        }
    }
    public override void Exit(Piece piece)
    {
        //base.Execute(piece);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        main_script._Hability.Use();
    }
}
