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
    }
    public override void Execute(Piece piece)
    {
        if (finished)
        {
            if (piece != null)
                _main_script._stateMachine.ChangeState(new DashState());
        }
      
    }
    public override void Exit(Piece piece)
    {
        atkList.Clear();
        //  base.Exit(piece);
    }

    IEnumerator CAttack()
    {
        int num = 0;
        while(num < atktimes)
        {
            Vector2 rnd = _main_script.RandomDirection();
            Vector2 posfinal = player.transform.position + ((Vector3)rnd * 2);
            atkList.Add(CreateAtkArea(posfinal));
            num++;
            yield return new WaitForSeconds(timebetweenatks);
        }
        finished = true;
        yield return null;
    }


}
