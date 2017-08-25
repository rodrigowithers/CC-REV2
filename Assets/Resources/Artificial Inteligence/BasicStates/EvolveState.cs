using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveState : State
{

    bool finished = false;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        main_script.SetColor(Color.white);
        main_script.IsInvincible = true;
        
        main_script.Level++;

        

        main_script.StartCoroutine(CWait());

    }
    public override void Execute(Piece piece)
    {
        //base.Execute(piece);
        //main_script._Hability.Use();
        //main_script._StateMachine.RevertToPreviousState();
        if(finished)
        {
            StateManager.Instance.AdjustFollow(piece.GetComponent<Enemy>());
        }

    }
    public override void Exit(Piece piece)
    {
        main_script.IsInvincible = false;

        //base.Execute(piece);
    }

    IEnumerator CWait()
    {
        yield return new WaitForSeconds(2);
        finished = true;
        //main_script._Hability.Use();
    }
}
