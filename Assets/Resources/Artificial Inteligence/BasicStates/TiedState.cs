using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiedState : State
{
    float normalspeed = 0;
    float speed_deducer = 0;
    public TiedState(int n)
    {
        speed_deducer = 1 - (n * 0.3f);
    }
    //GameObject _player_ref;
    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);

        main_script.SetColor(Color.blue);
        normalspeed = main_script.Speed;
        main_script.Speed *= speed_deducer;
        // main_script.StartCoroutine(CPromove());

    }
    public override void Execute(Piece piece)
    {
        main_script.Direction = main_script.AtkAreaDirPlayer();
        //main_script.Move(main_script.Direction);
        main_script.Move();
        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        main_script.Speed = normalspeed;

        base.Exit(piece);
    }

    //IEnumerator CPromove()
    //{
    //    yield return new WaitForSeconds(1);
    //    PieceManager.Instance.PromoveEnemy(main_script);
    //}

}
