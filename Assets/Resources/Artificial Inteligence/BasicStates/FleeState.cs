using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FleeState : State
{
    float fleetime = 3;
    Vector2 runto = Vector2.zero;
    bool finished = false;
    bool inwalls = false;

    //GameObject _player_ref;
    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);
        main_script.Speed *= 3;
        main_script.SetColor(Color.blue);
        main_script.StartCoroutine(CTimer());
    }
    public override void Execute(Piece piece)
    {
        if(main_script.CheckNearWalls())
        {
            if(IsCornered())
            {
                main_script._StateMachine.ChangeState(new FollowPathState());
                finished = true;
                //main_script.Move(DesperateDirection());
            }
            else
                main_script.Move(CheckDirection());         
        }
        else
            main_script.Move(-main_script.EnemyDirPlayer());

        if(finished)
        {
            main_script._StateMachine.ChangeState(new FollowPathState());
        }
        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        main_script.Speed /= 3;
        base.Exit(piece);
    }

    Vector2 CheckDirection()
    {
        Vector2 toreturn;
        if (main_script.transform.position.x > 0)
            toreturn.x = 1;
        else
            toreturn.x = -1;

        if (main_script.transform.position.y > Camera.main.transform.position.y)
            toreturn.y = 1;
        else
            toreturn.y = -1;

        return toreturn;
    }

    IEnumerator CTimer()
    {
        yield return new WaitForSeconds(fleetime);
        finished = true;
       // yield return null;
    }
    
    Vector2 DesperateDirection()
    {
        Vector2 toreturn = Vector2.zero;

        if (IsCornered())
        {
            toreturn = CornerDecision();
            return toreturn;
        }
        else
        {

        }


        return toreturn;
    }

    bool IsCornered()
    {
        Vector3 cam = Camera.main.transform.position;
        Vector2 pos = main_script.transform.position;
        if (pos.x > 16 && pos.y < cam.y - 8)
            return true;
        if (pos.x > 16 && pos.y > cam.y + 8)
            return true;
        if (pos.x < -16 && pos.y < cam.y - 8)
            return true;
        if (pos.x < -16 && pos.y > cam.y + 8)
            return true;

        return false;
    }
    Vector2 CornerDecision()
    {
        Vector2 toreturn = Vector2.zero;

        Vector2 pos = main_script.transform.position;
        if (pos.x > 16)
            toreturn.x = -1;
        if (pos.x < -16)
            toreturn.x = 1;
        return toreturn;
    }
}
