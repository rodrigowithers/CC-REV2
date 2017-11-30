using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverState : StayStillState {


    public override void Enter(Piece piece)
    {
        time_stopped = 5;
        //_main_script.IsInvincible = false;
        base.Enter(piece);
        //_main_script._lifebar.SetActive(true);

    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
            {
                //_main_script._lifebar.SetActive(false);
                _main_script.IsInvincible = true;
                _main_script._stateMachine.ChangeState(new WalkAroundState());
            }
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }
}
