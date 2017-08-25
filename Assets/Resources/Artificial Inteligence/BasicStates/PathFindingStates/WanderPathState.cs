using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderPathState : State
{

    float max_times_walked = 3;
    bool is_changing = false;
    float time_to_next_walk = 2;



    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        unit = main_script.GetComponent<Unit>();
        Type = STATETYPE.WANDER;
        //main_script.IsAware = false;
        main_script.SetColor(Color.green);
        //main_script._currentattacking = false;
        main_script.Use_Path = true;
        
    }

    public override void Execute(Piece piece)
    {
 
       if(!is_changing)
        {
            is_changing = true;
            main_script.StartCoroutine(TimeToChange());
        }

        if (SeenPlayer() || max_times_walked < 0)
        {
            main_script._StateMachine.ChangeState(new AwareState());
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }


    IEnumerator TimeToChange()
    {
        yield return new WaitForSeconds(time_to_next_walk);

        unit.StartCoroutine(unit.RequestNewPathTo(new Vector2(-1000, 0)));
        is_changing = false;
        max_times_walked--;

        yield return null;
    }
    
    bool SeenPlayer()
    {
        Vector2 target_dir = main_script.EnemyDirPlayer();

        if (Vector2.Dot(main_script.Direction, target_dir) > 0.6f)
        {
            if (main_script.EnemyDistPlayer() < 10)
                return true;
        }
        return false;
    }
}
