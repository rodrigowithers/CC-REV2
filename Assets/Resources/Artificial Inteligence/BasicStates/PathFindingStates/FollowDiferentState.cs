using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDiferentState : State
{

    public override void Enter(Piece piece)
    {
        base.Enter(piece);

        main_script.SetColor(Color.red);

        main_script.Use_Path = true;
        unit = main_script.GetComponent<Unit>();
    }
    
    public override void Execute(Piece piece)
    {
        if (main_script.Smart)
        {
            if ((player.transform.position - main_script.transform.position).magnitude <= 6)
            {
                StateManager.Instance.AdjustHunt(main_script);
            }
            else
                unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position - (Vector3)main_script.AtkAreaToEnemy()));
        }
        else
        {
            if (main_script.AtkAreaDistPlayer() <= main_script.GetClass.atkareacheck)
            {
                main_script._StateMachine.ChangeState(new PrepareAttackState());
            }
            unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position - (Vector3)main_script.AtkAreaToEnemy()));
        }

        //CheckforDanger();
        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }
}