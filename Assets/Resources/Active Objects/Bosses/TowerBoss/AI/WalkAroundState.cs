using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAroundState : State
{
    bool finished = false;
    float walkRadius = 3;
    Vector2 Center = Camera.main.transform.position;
    TowerBoss _main_script;
    Vector2 dir;
    Vector2 destination;
    public override void Enter(Piece piece)
    {
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.CanMove = true;
        destination = ChooseDestination();
        dir = DirectionToDestination();

        _main_script.StartCoroutine(CWalkTime());
        _main_script.RigidBody.velocity = Vector2.zero;

    }
    public override void Execute(Piece piece)
    {
        if(DistanceLeft() <= 0)
        {
            finished = true;
            _main_script.StopCoroutine(CWalkTime());
        }
        if (finished)
        {
            if (piece != null)
                StateChange();
                //_main_script._stateMachine.ChangeState(new AccurateAtkState());
        }
        _main_script.Move(dir * _main_script.Speed);               
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }

    float DistanceLeft()
    {
        return ((Vector3)destination - _main_script.transform.position).magnitude;
    }
    Vector2 DirectionToDestination()
    {
        Vector2 toreturn = ((Vector3)destination - _main_script.transform.position).normalized; 
        return toreturn;
    }
    Vector2 ChooseDestination()
    {
        Vector2 random = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        random *= walkRadius;        
        return Center + random;
    }

    IEnumerator CWalkTime()
    {
        yield return new WaitForSeconds(3);
        finished = true;
    }
    

    void StateChange()
    {
        StateMachine SM = _main_script._stateMachine;

        if (Random.Range(0, 2) == 0 && !(SM.GetPreviousState() is WalkAroundState) && !(SM.GetPreviousState() is RecoverState))
        {
            SM.ChangeState(new WalkAroundState());
        }
        else
        {
            if (SM.GetPreviousState() is GiantAtkState || SM.GetPreviousState() is WalkAroundState)
                SM.ChangeState(new AccurateAtkState());
            else if (SM.GetPreviousState() is AccurateAtkState || SM.GetPreviousState() is DashState)
                SM.ChangeState(new SpawnMinionState());
            else if (SM.GetPreviousState() is DashState)
                SM.ChangeState(new ShowerAtkState());
            else if (SM.GetPreviousState() is RecoverState)
                SM.ChangeState(new SceneShowerAtkState());
        }
    }

}

