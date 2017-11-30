using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwareState : State
{
    bool finished = false;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);

        //main_script.IsAware = true;
        piece.RigidBody.velocity = Vector2.zero;

        main_script.SetColor(Color.grey);
        main_script.StartCoroutine(ShowExclamation(piece));
        main_script.Use_Path = false;
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
                main_script._StateMachine.ChangeState(new FollowPathState());
                   
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }

    IEnumerator ShowExclamation(Piece p)
    {
        Vector2 posInScreen = Camera.main.WorldToViewportPoint(p.transform.position);
        posInScreen += new Vector2(0, 1);
        float time = 0.5f;
        //main_script.HudCreator.CreateExclamationBubble(p.transform.position);
        // Avisa os outros inimigos
        //WaveManager._instance.Scenes[WaveManager._instance.CurrentScene].MakeEnemiesAware();

        yield return new WaitForSeconds(time);

        //Managerscript.instance.PlayerSighted();
        //yield return new WaitForSeconds(time);
        finished = true;
        yield return null;
    }



    //void ChangeStateTo()
    //{
    //    if(main_script._Level == LEVEL.LESSER)
    //    {
    //        main_script._StateMachine.ChangeState(new FollowState());
    //        return;
    //    }
    //    main_script._StateMachine.ChangeState(new T_FollowState());

    //}
}
