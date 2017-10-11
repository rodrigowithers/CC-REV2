using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tower : Enemy
{
    public string SpawnType = null;

    public override void Die()
    {
        base.Die();
    }

    private void Awake()
    {
        if(SpawnType != null)
            PieceManager.Instance.ChangeClass(this, System.Type.GetType(SpawnType));
    }
    void Start()
    {
        base.Start();
        statemachine = GetComponent<StateMachine>();
        statemachine.StartManchine(this);

        //statemachine.ChangeState(new WanderState());
        statemachine.ChangeState(new HuntState());
        VerifyLevel();

        //_class = (Class) gameObject.AddComponent<Bishop_Normal>();
    }

    void Update()
    {
        base.Update();
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
