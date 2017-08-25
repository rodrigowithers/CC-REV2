using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public class ConfusedState : State
{
    bool finished = false;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        //main_script._currentattacking = false;

       // if (main_script.kingeffectiveness < 3)
        //    main_script.kingeffectiveness++;

        //main_script.IsAware = true;
        Type = STATETYPE.WANDER;
        //Debug.Log("e agora???");
        main_script.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        main_script.SetColor(Color.blue);
        main_script.StartCoroutine(CShowInterrogation(piece));
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            //main_script._isconfused = false;

            StateManager.Instance.AdjustFollow(piece.GetComponent<Enemy>());
            //main_script.StateMachine.ChangeState(new FollowState());

        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }

    IEnumerator CShowInterrogation(Piece p)
    {
        main_script.SetColor(Color.blue);
        Vector2 posInScreen = Camera.main.WorldToViewportPoint(p.transform.position);
        posInScreen += new Vector2(0, 1);
        float time = 1;
        //float time = 1.6f - (0.4f * main_script.kingeffectiveness);
       // main_script.HudCreator.CreateInterrogationBubble(p.transform.position);
        // Avisa os outros inimigos
        //WaveManager._instance.Scenes[WaveManager._instance.CurrentScene].MakeEnemiesAware();

        yield return new WaitForSeconds(time);

        //Managerscript.instance.PlayerSighted();
        finished = true;
        yield return null;
    }
}
