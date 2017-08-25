using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
public class PathFinding : MonoBehaviour
{
    Grid grid;
    void Awake()
    {
        grid = GetComponent<Grid>();
    }



    public void IFindPath(PathRequest request, Action<PathResult> callback)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Vector2[] waypoint = new Vector2[0];
        bool pathSucess = false;
        Node startNode = grid.NodeFromWorldPoint(request.pathStart);
        Node endNode = grid.NodeFromWorldPoint(request.pathEnd);
        if (startNode.walkable && endNode.walkable)
        {
            Heap<Node> openset = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedset = new HashSet<Node>();
            openset.Add(startNode);

            while (openset.Count > 0)
            {
                Node currentNode = openset.RemoveFirst();

                closedset.Add(currentNode);
                if (currentNode == endNode)
                {
                    sw.Stop();
                    //print("Path found in: " + sw.ElapsedMilliseconds + "ms");
                    pathSucess = true;
                    break;
                }
                foreach (Node neightbour in grid.GetNeightbours(currentNode))
                {
                    if (!neightbour.walkable || closedset.Contains(neightbour))
                        continue;

                    int newMovementCostToNeightbour = currentNode.G_cost + GetDistance(currentNode, neightbour) + neightbour.penaltyValue;
                    if (newMovementCostToNeightbour < neightbour.G_cost || !openset.Contains(neightbour))
                    {
                        neightbour.G_cost = newMovementCostToNeightbour;
                        neightbour.H_cost = GetDistance(neightbour, endNode);
                        neightbour.parent = currentNode;
                        if (!openset.Contains(neightbour))
                            openset.Add(neightbour);
                        else
                            openset.UpdateItem(neightbour);
                    }
                }
            }
        }
        if(pathSucess)
        {
            waypoint = RetracePath(startNode,endNode);
        }
        callback(new PathResult(waypoint, pathSucess, request.callback));
}
    
    public void IFindRandomPath(PathRequest request, Action<PathResult> callback)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Vector2[] waypoint = new Vector2[0];
        bool pathSucess = false;
        Node startNode = grid.NodeFromWorldPoint(request.pathStart);
        Node endNode = grid.RandomWalkableNode();

        if (startNode.walkable && endNode.walkable)
        {
            Heap<Node> openset = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedset = new HashSet<Node>();
            openset.Add(startNode);

            while (openset.Count > 0)
            {
                Node currentNode = openset.RemoveFirst();

                closedset.Add(currentNode);
                if (currentNode == endNode)
                {
                    sw.Stop();
                    print("Path found in: " + sw.ElapsedMilliseconds + "ms");
                    pathSucess = true;
                    break;
                }
                foreach (Node neightbour in grid.GetNeightbours(currentNode))
                {
                    if (!neightbour.walkable || closedset.Contains(neightbour))
                        continue;

                    int newMovementCostToNeightbour = currentNode.G_cost + GetDistance(currentNode, neightbour) + neightbour.penaltyValue;
                    if (newMovementCostToNeightbour < neightbour.G_cost || !openset.Contains(neightbour))
                    {
                        neightbour.G_cost = newMovementCostToNeightbour;
                        neightbour.H_cost = GetDistance(neightbour, endNode);
                        neightbour.parent = currentNode;
                        if (!openset.Contains(neightbour))
                            openset.Add(neightbour);
                        else
                            openset.UpdateItem(neightbour);
                    }
                }
            }
        }
        if (pathSucess)
        {
            waypoint = RetracePath(startNode, endNode);
        }
        callback(new PathResult(waypoint, pathSucess, request.callback));
        
    }
    
    Vector2[] RetracePath(Node Start, Node End)
    {
        List<Node> path = new List<Node>();
        Node currentNode = End;
        while (currentNode != Start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector2[] waypoint = SimplifyPath(path);

        Array.Reverse(waypoint);

        return waypoint;
    }

    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionold = Vector2.zero;
        for(int i = 1;i<path.Count;i++)
        {
            Vector2 DirectionNew = new Vector2(path[i - 1].gridx - path[i].gridx, path[i - 1].gridy - path[i].gridy);
            if(DirectionNew != directionold)
            {
                waypoints.Add(path[i].worldPosition);
                directionold = DirectionNew;
            }
        }

        return waypoints.ToArray();
    }


    int GetDistance(Node A, Node B)
    {
        int distx = Mathf.Abs(A.gridx - B.gridx);
        int disty = Mathf.Abs(A.gridy - B.gridy);

        if (distx > disty)
            return 14 * disty + 10 * (distx - disty);
        return 14 * distx + 10 * (disty - distx);
    }

    public Grid GetGrid()
    {
        return grid;
    }



}
