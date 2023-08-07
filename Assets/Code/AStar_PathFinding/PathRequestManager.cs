using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class PathRequestManager : MonoBehaviour {

    Queue<PathResult> ResultsQueue = new Queue<PathResult>();
   
    static PathRequestManager instance;
    PathFinding pathfinding;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathFinding>();
    }

    void Update()
    {
        if(ResultsQueue.Count>0)
        {
            int ItemsinQueue = ResultsQueue.Count;
            lock(ResultsQueue)
            {
                for(int i = 0; i< ItemsinQueue;i++)
                {
                    PathResult result = ResultsQueue.Dequeue();
                    result.callback(result.path,result.success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest request)
    {

        ThreadStart threadstart = delegate
        {
            instance.pathfinding.IFindPath(request, instance.FinishedProcessingPath);
        };
        threadstart.Invoke();
    }
    
    public void FinishedProcessingPath(PathResult result)
    {
        lock (ResultsQueue) {
            ResultsQueue.Enqueue(result);
        }

    }

    public static bool VerifyLocation(Vector2 position)
    {
        return instance.pathfinding.GetGrid().IsLocationPossible(position);
    }

}

public struct PathResult
{
    public Vector2[] path;
    public bool success;
    public Action<Vector2[], bool> callback;
    public PathResult(Vector2[] p, bool s, Action<Vector2[], bool> action)
    {
        path = p;
        success = s;
        callback = action;
    }
}

public struct PathRequest
{
    public Vector2 pathStart;
    public Vector2 pathEnd;
    public Action<Vector2[], bool> callback;
    public PathRequest(Vector2 start, Vector2 end, Action<Vector2[], bool> action)
    {
        pathStart = start;
        pathEnd = end;
        callback = action;
    }
}