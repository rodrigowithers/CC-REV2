using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAtkState : State {

    bool finished = false;
    TowerBoss _main_script;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.CanMove = false;
        _main_script.StartCoroutine(CAttack());
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
                _main_script._stateMachine.ChangeState(new WalkAroundState());
        }

    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }
    IEnumerator CAttack()
    {
        yield return new WaitForSeconds(4);
        int side = 1;
        if (player.transform.position.x < _main_script.transform.position.x)
            side = -1;
        GameObject area = GameObject.Instantiate(_main_script._atkArea);
        area.transform.position = Camera.main.transform.position + new Vector3(side * 9,0,10);
        area.transform.localScale = new Vector3(5,5,0);
        

        finished = true;
        yield return null;
    }
}
