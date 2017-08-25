using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidDangerState : State
{
    GameObject ObjofAwareness;
    List<Vector2> PosfInterest = new List<Vector2>();
    int actualposofinterest = 0;
    //GameObject _player_ref;
    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);
        var hits = Physics2D.RaycastAll(main_script.transform.position, main_script.EnemyDirPlayer(), 5);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<FloorSpike>() != null)
            {
                ObjofAwareness = hit.collider.gameObject;
            }
        }
        AnalizeObject();
    }
    public override void Execute(Piece piece)
    {
        base.Execute(piece);

        if(DistFromInterest() < 0.5f)
        {
            actualposofinterest++;
            if (actualposofinterest >= PosfInterest.Count)
                StateManager.Instance.AdjustFollow(main_script);
        }

        main_script.Move(DirtoInterest());

    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    Vector2 DirtoInterest()
    {
        return PosfInterest[actualposofinterest] - (Vector2)main_script.transform.position;
    }

    float DistFromInterest()
    {
        return DirtoInterest().magnitude;
    }


    void AnalizeObject()
    {
        bool finished = false;
        AddPointofInterest(main_script.transform.position);
        AddPointofInterest(GameManager.Instance._Player.transform.position);
        Vector2 actual = Vector2.zero;
        Vector2 next   = Vector2.zero;
        do
        {
            for(int i = 0;i < PosfInterest.Count;i++)
            {
                actual = PosfInterest[i];
                next = PosfInterest[i + 1];
                if (checkraycast(actual,next))
                {
                    InsertPointofInterest(i,VectortoLine(actual,next));
                    break;
                }

                if(i == PosfInterest.Count-1)
                {
                    finished = true;
                }
            }


        } while (!finished);
        
    }

    bool checkraycast(Vector2 pos1 , Vector2 pos2)
    {
        Vector2 dir = pos2 - pos1;
        float dist = dir.magnitude;
        var hits = Physics2D.RaycastAll(pos1, dir.normalized, dist);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<FloorSpike>() != null)
            {
                return true;
            }
        }

        return false;
    }


    void AddPointofInterest(Vector2 v)
    {
        PosfInterest.Add(v);
        //PosfInterest.Insert(v);
    }
    void InsertPointofInterest(int index ,Vector2 v)
    {
        PosfInterest.Insert(index,v);
        //PosfInterest.Insert(v);
    }

    Vector2 VectortoLine(Vector2 pos1, Vector2 pos2)
    {
        Vector2 pldir = pos2 - pos1;
        Vector2 pdir = (Vector2)ObjofAwareness.transform.position - pos1;
        float pdist = pdir.magnitude;

        float rad = Vector2.Dot(pldir.normalized, pdir.normalized);
        float angle = Mathf.Rad2Deg * rad;

        

        float ldist = Mathf.Sin(angle) * pdist;

        Vector2 point = pldir * ldist;

        Vector2 dir = point - (Vector2)ObjofAwareness.transform.position;
        return point + dir;
    }


}

