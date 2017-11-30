using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerAtkState : AccurateAtkState {

    List<GameObject> atkList = new List<GameObject>();
    float timebetweenatks = 0.5f;
    public override void Enter(Piece piece)
    {
        base.Enter(piece);
        atktimes = 7;
        _main_script.StartCoroutine(CAttack());

        // Toca a animação
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Prepare", 0, true,true);
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
            {
                if(_main_script._life < 10)
                    _main_script._stateMachine.ChangeState(new GiantAtkState());
                else
                    _main_script._stateMachine.ChangeState(new DashState());
            }
        }
      
    }
    public override void Exit(Piece piece)
    {
        atkList.Clear();
        //  base.Exit(piece);
    }

    IEnumerator CAttack()
    {
        yield return new WaitForSeconds(0.5f);

        // Toca a animação
        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Special", 0, true, true);

        int num = 0;
        while(num < atktimes)
        {
            Vector2 rnd = _main_script.RandomDirection();
            Vector2 posfinal = player.transform.position + ((Vector3)rnd * 2);
            atkList.Add(CreateAtkArea(posfinal));
            num++;
            yield return new WaitForSeconds(timebetweenatks);
        }

        _main_script.GetComponent<ClassAnimator>().Stop();
        _main_script.GetComponent<ClassAnimator>().Play("Idle", 0, true, true);
        finished = true;
        yield return null;
    }


}
