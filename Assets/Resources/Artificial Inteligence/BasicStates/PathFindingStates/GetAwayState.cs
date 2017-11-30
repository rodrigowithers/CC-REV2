using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAwayState : State
{

    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);

        main_script.Use_Path = true;
        unit = main_script.GetComponent<Unit>();
        player = GameManager.Instance._Player;
        unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position));

    }
    public override void Execute(Piece piece)
    {


        // Cancela todas as corrotinas da unidade e inicia uma nova de perseguir o player
        unit.StopAllCoroutines();

        if (main_script.Smart)
        {
            if ((player.transform.position - main_script.transform.position).magnitude <= 6)
            {
                // player ja chegou perto 
                // muda de estado 
                //main_script._StateMachine.ChangeState(new PawnHuntState());    
                StateManager.Instance.AdjustHunt(main_script);
            }
            else
                unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position));
        }
        else
        {
            if (main_script.AtkAreaDistPlayer() <= 0.5f)
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
