using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{

    float test = 3;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        main_script.SetColor(Color.magenta);
        //main_script.StartCoroutine(wait());

    }
    public override void Execute(Piece piece)
    {
        //base.Execute(piece);
        main_script._Hability.Use();
        main_script._StateMachine.RevertToPreviousState();

    }
    public override void Exit(Piece piece)
    {
        //base.Execute(piece);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(test);
        main_script._Hability.Use();
    }
}

