using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAtkState : State {

    bool finished = false;
    TowerBoss _main_script;
    float tillareaexplode = 1.3f;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        _main_script = piece.GetComponent<TowerBoss>();
        _main_script.RigidBody.velocity = Vector2.zero;
        _main_script.CanMove = false;
        _main_script.StartCoroutine(CAttack());

        // Toca a animação
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Prepare", 0, true, true);
    }
    public override void Execute(Piece piece)
    {
        if (_main_script.transform.position.x > 16)
        {
            _main_script.RigidBody.velocity = Vector2.zero;
        }
        else if (_main_script.transform.position.x < -16)
        {
            _main_script.RigidBody.velocity = Vector2.zero;
        }
        if (finished)
        {
            if (piece != null)
            {
                _main_script.IsInvincible = false;
                _main_script._stateMachine.ChangeState(new RecoverState());
            }
        }
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }
    IEnumerator CAttack()
    {
        yield return new WaitForSeconds(2);
        int side = 1;
        if (player.transform.position.x < _main_script.transform.position.x)
            side = -1;
        GameObject area = GameObject.Instantiate(_main_script._atkArea);
        area.transform.position = Camera.main.transform.position + new Vector3(side * 9,0,10);
        area.transform.localScale = new Vector3(5,5,0);
        area.GetComponent<TowerBossAttackArea>().ExplosionTimer = tillareaexplode;
        area.GetComponent<TowerBossAttackArea>().special = true;

        _main_script.StartCoroutine(AfterAttack(side));
        yield return null;
    }

    IEnumerator AfterAttack(int s)
    {
        yield return new WaitForSeconds(tillareaexplode);

        Camera.main.GetComponent<CameraController>().Shake(1.0f);
        Camera.main.GetComponent<Flash>().StartFlash();

        _main_script.RigidBody.velocity = Vector2.right * s * -1 * 15;


        // Toca a animação
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Dizzy", 0, true, true);

        yield return new WaitForSeconds(0.5f);

        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("DizzyLoop", 0, true, true);



        yield return new WaitForSeconds(0.3f);

        finished = true;
        yield return null;
    }



}
