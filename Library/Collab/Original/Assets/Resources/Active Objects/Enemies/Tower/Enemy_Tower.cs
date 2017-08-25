using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tower : Enemy
{

    public override void Die()
    {
        base.Die();
    }

    void Start()
    {
        PieceManager.Instance.ChangeClass(this, typeof(Pawn_Normal));

        base.Start();
        statemachine = GetComponent<StateMachine>();
        statemachine.StartManchine(this);
        statemachine.ChangeState(new WanderState());
        VerifyLevel();
    }

    void Update()
    {

    }

    public override void VerifyLevel()
    {
        if (Level == LEVEL.LESSER)
        {
            //Colocar aqui dentro todas as modificações que farão de um Normal Enemy um Lesser Enemy 
            Speed /= 2;
        }
        else if (Level == LEVEL.GREATER)
        {
            //Colocar aqui dentro todas as modificações que farão de um Normal Enemy um Greater Enemy 
            Speed *= 2;
        }
    }
}
