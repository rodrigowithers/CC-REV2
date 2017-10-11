using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State {

    //GameObject _player_ref;
    public override void Enter(Piece piece)
    {
       // _player_ref = GameManager.Instance._player;
        base.Enter(piece);

        main_script.SetColor(Color.red);
       // main_script.StartCoroutine(CPromove());

    }
    public override void Execute(Piece piece)
    {
        main_script.Direction = main_script.AtkAreaDirPlayer();
        //main_script.Move(main_script.Direction);
        main_script.Move();

        if (main_script.AtkAreaDistPlayer() < 0.5f)
        {
            main_script._StateMachine.ChangeState(new PrepareAttackState());
        }
        //if(main_script.CheckNearWalls() && main_script.EnemyDistPlayer() < 6)
        //{
        //    StateManager.Instance.AdjustFlee(main_script);
        //}

        // Checa se vai dar ruim


        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }



    bool CheckForAreaDangers()
    {
        var hits = Physics2D.RaycastAll(main_script.transform.position, main_script.EnemyDirPlayer(), 5);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<FloorSpike>() != null)
            {
                return true;
            }
        }
        return false;
    }



    //// será usada pelas classes filhas de follow
    protected bool PlayerIsRanged()
    {
        CHESSPIECE T = GameManager.Instance._Player.GetComponent<Player>().GetClass.Type;
        if (T == CHESSPIECE.QUEEN || T == CHESSPIECE.TOWER || T == CHESSPIECE.BISHOP)
            return true;

        return false;
    }

    //protected void CheckNearWalls()
    //{
    //    Vector3 cam = Camera.main.transform.position;
    //    Vector3 pos = main_script.transform.position;
    //    if()
    //}



}
