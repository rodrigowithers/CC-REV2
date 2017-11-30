using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{

    float time_to_change_dir = 0.1f;
    bool is_changing = false;

    public override void Enter(Piece piece)
    {
        //Debug.Log("CHOREI");
        base.Enter(piece);
        Type = STATETYPE.WANDER;
        //main_script.IsAware = false;
        main_script.SetColor(Color.green);
        //main_script._currentattacking = false;

    }

    public override void Execute(Piece piece)
    {
        if (piece == null || main_script == null)
            return;

        if (!is_changing)
        {
            main_script.StartCoroutine(TimeToChange(piece));
            is_changing = true;
        }

        main_script.Move(main_script.Direction);

        if (main_script.IsAttacking)
        {
            main_script._StateMachine.ChangeState(new AwareState());
            //if (SeenPlayer())
            //{
            //    main_script._StateMachine.ChangeState(new AwareState());
            //}
        }

    }
    public override void Exit(Piece piece)
    {
        //  base.Exit(piece);
    }


    IEnumerator TimeToChange(Piece piece)
    {
        yield return new WaitForSeconds(time_to_change_dir);

        Vector2 change = DirectionChange(piece);

        main_script.Direction += change;

        main_script.Direction.Normalize();

        is_changing = false;
        yield return null;
    }


    Vector2 DirectionChange(Piece piece)
    {
        float x = 0, y = 0;
        Vector2 posInScreen = Camera.main.WorldToViewportPoint(piece.transform.position);

        ///////////////////////////////////// CHECANDO O X
        if (posInScreen.x < 0.1f)// está na borda esquerda
        {
            x = Random.Range(0.5f, 1.0f);
        }
        else if (posInScreen.x > 0.1f && posInScreen.x <= 0.3f)// está quase na  borda esquerda
        {
            x = Random.Range(-0.3f, 0.7f);
        }
        else if (posInScreen.x > 0.3f && posInScreen.x <= 0.7f)// está quase no meio 
        {
            x = Random.Range(-0.5f, 0.5f);
        }
        else if (posInScreen.x > 0.7f && posInScreen.x <= 0.9f)// está quase na  borda direita
        {
            x = Random.Range(-0.7f, 0.3f);
        }
        else if (posInScreen.x > 0.9f) // está na borda direita
        {
            x = Random.Range(-1.0f, -0.5f);
        }
        //////////////////////////////////////////// CHECANDO O Y
        if (posInScreen.y < 0.1f)// está na borda de cima
        {
            y = Random.Range(0.5f, 1.0f);
        }
        else if (posInScreen.y > 0.1f && posInScreen.y <= 0.3f)// está quase na  borda de cima
        {
            y = Random.Range(-0.3f, 0.7f);
        }
        else if (posInScreen.y > 0.3f && posInScreen.y <= 0.7f)// está quase no meio 
        {
            y = Random.Range(-0.5f, 0.5f);
        }
        else if (posInScreen.y > 0.7f && posInScreen.y <= 0.9f)// está quase na  borda de baixo
        {
            y = Random.Range(-0.7f, 0.3f);
        }
        else if (posInScreen.y > 0.9f) // está na borda de baixo
        {
            y = Random.Range(-1.0f, -0.5f);
        }

        Vector2 change = new Vector2(x, y);

        return change;
    }



    bool SeenPlayer()
    {
        Vector2 target_dir = main_script.EnemyDirPlayer();

        if (Vector2.Dot(main_script.Direction, target_dir) > 0.6f)
        {
            if (main_script.EnemyDistPlayer() < 10)
                return true;
        }
        return false;
    }
}
