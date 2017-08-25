using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareAttackState : State
{
    bool finished = false;
    bool finishedagain = false;

    float minimudistance = 0.5f;
    Transform atkarea;

    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        atkarea = main_script.CurrentAtkArea;

        main_script.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        main_script.SetColor(Color.cyan);
        main_script.StartCoroutine(CPreparingAttack());
    }
    public override void Execute(Piece piece)
    {
        if (finished )
        {
            main_script.GetClass.Attack(main_script.EnemyDirPlayer());
            main_script.StartCoroutine( CQuittingAttack());
        }
        if(main_script.AtkAreaDistPlayer() > minimudistance)
        {
            main_script.StartCoroutine(CQuittingAttack());
        }
        if(finishedagain)
        {
            StateManager.Instance.AdjustFollow(piece.GetComponent<Enemy>());
            //main_script._StateMachine.ChangeState(new FollowState());
        }
   
    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }

    IEnumerator CPreparingAttack()
    {
     
        yield return new WaitForSeconds(0.1f);

        //Managerscript.instance.PlayerSighted();
        finished = true;
        yield return null;
    }

    IEnumerator CQuittingAttack()
    {

        yield return new WaitForSeconds(0.5f);

        //Managerscript.instance.PlayerSighted();
        finishedagain = true;
        yield return null;
    }

}
