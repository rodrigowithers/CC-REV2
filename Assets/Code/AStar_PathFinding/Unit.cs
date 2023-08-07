﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveTreshHold = 0.5f;


    Vector2 Target = Vector2.zero;

    public float speed = 1;
    public float turnSpd = 1;

    public bool WillFollowPath = true;

    Path path;
    Enemy main_script;

    void Awake()
    {
        //Target = GameObject.FindGameObjectWithTag("Player");


        main_script = GetComponent<Enemy>();

        // StartCoroutine(UpdatePath());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    Coroutine follow;

    public void OnPathFound(Vector2[] waypoints, bool pathsucessful)
    {
        if (pathsucessful && waypoints != null && main_script.CanMove)
        {
            path = new Path(waypoints, transform.position, turnSpd);

            if(follow == null)
            {
                follow = StartCoroutine(FollowPath());
            }
            else
            {
                //Debug.Log("Reiniciando Follow");

                StopCoroutine(follow);
                follow = StartCoroutine(FollowPath());
            }

            //StopAllCoroutines();
            //StopCoroutine(FollowPath());
            //StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        //Debug.Log("Iniciou Nova Paradinha");

        bool following = true;
        int pathindex = 0;

        while (main_script.Use_Path && following)
        {
            // Checa se pode mover
            if (!main_script.CanMove)
            {
                yield break;
            }

            if (path.turnBoundaries.Length != 0)
            {

                if(path.turnBoundaries.Length > pathindex)
                {
                    while (path.turnBoundaries[pathindex].HasCrossedLine(transform.position) && main_script.CanMove)
                    {
                        if (pathindex == path.finishLineIndex)
                        {
                            following = false;
                            break;
                        }
                        else
                        {
                            pathindex++;
                        }
                    }
                }
            }
            if (following && main_script.Use_Path)
            {
                if (pathindex >= path.lookPoints.Length)
                    yield break;
                else
                {
                    Vector2 dir = (path.lookPoints[pathindex] - (Vector2)transform.position).normalized;

                    //yield return new WaitForSeconds(1.0f);

                    //Debug.Log("Moving...");

                    //transform.position = path.lookPoints[pathindex];

                    main_script.Move(dir);
                    //transform.Translate(dir * Time.deltaTime);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }


    //IEnumerator UpdatePath()
    //{
    //    if (WillFollowPath)
    //    {
    //        if (Time.timeSinceLevelLoad < .3f)
    //        {
    //            yield return new WaitForSeconds(.3f);
    //        }
    //        //PathRequestManager.RequestPath(new PathRequest(transform.position, Target.position, OnPathFound));
    //        StartCoroutine( RequestNewPathTo());

    //        float sqrMoveTreshHold = pathUpdateMoveTreshHold * pathUpdateMoveTreshHold;
    //        Vector2 oldPos = transform.position;
    //        while (true)
    //        {
    //            yield return new WaitForSeconds(minPathUpdateTime);
    //            //if (((Vector2)Target.transform.position - oldPos).sqrMagnitude > sqrMoveTreshHold)
    //            //{
    //            //    StartCoroutine(RequestNewPathTo());
    //            //    //PathRequestManager.RequestPath(new PathRequest(transform.position, Target.position, OnPathFound));
    //            //    oldPos = Target.transform.position;
    //            //}
    //            if(( (Vector2)Target.transform.position - (Vector2)transform.position).magnitude<2)
    //            {
    //                StartCoroutine(RequestNewPathTo());

    //            }
    //        }
    //    }
    //}

    public bool FinishedPath()
    {
        Debug.Log((Target - (Vector2)transform.position).magnitude < 2);

        if ((Target - (Vector2)transform.position).magnitude < 2)
        {
            return true;
        }
        return false;
    }


    public IEnumerator RequestNewPathTo(GameObject target = null)
    {
        if (target == null)
        {
            bool found = false;
            Transform cam = Camera.main.transform;
            //Vector2 selectedpos = Vector2.zero;
            Vector2 selectedpos = new Vector2(5, 5);
            while (!found)
            {
                //selectedpos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 0));
                //selectedpos = Camera.main.ViewportToWorldPoint(new Vector3(0,0, 0));

                if (PathRequestManager.VerifyLocation(selectedpos))
                {
                    found = true;
                }

            }


        }
        else
        {
        }

        //PathRequestManager.RequestPath(new PathRequest(transform.position, Target.transform.position, OnPathFound));

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator RequestNewPathTo(Vector2 pos)
    {
        DebugExtension.DebugCircle(pos, Vector3.forward, Color.blue, 0.2f);

        Vector2 cam = Camera.main.transform.position;
        if (pos.x == -1000)
        {
            bool found = false;
            while (!found)
            {
                Target = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0));
                if (!PathRequestManager.VerifyLocation(Target))
                {
                    Debug.Log("Não é possivel chegar à posição de destino");
                }
                else
                {
                    found = true;
                }
            }
        }
        else
        {
            Target = pos;
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, Target, OnPathFound));

        yield return new WaitForEndOfFrame();
    }

    void OnDrawGizmos()
    {
        if (path != null)
        {
            //path.DrawWithGizmos();
            Gizmos.color = Color.black;
            Vector2[] lp = path.GetLookPoints();
            foreach (Vector2 p in lp)
            {
                Gizmos.DrawCube(p, Vector3.one * 0.1f);
            }

            Gizmos.color = Color.gray;
            Line[] lines = path.GetTurnBoundaries();
            foreach (Line l in lines)
            {
                l.DrawWithGizmos(10);
            }
        }
    }

}



