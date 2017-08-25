using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSafePath : State
{

    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);

        main_script.SetColor(Color.red);

        main_script.Use_Path = true;
        unit = main_script.GetComponent<Unit>();
        Obtain_Destination();

    }
    public override void Execute(Piece piece)
    {

        if(unit.FinishedPath())
        {

        }
        
        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    


    void Obtain_Destination()
    {
        unit.RequestNewPathTo(new Vector2(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f))); 
    }



   

}