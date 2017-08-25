using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node :IHeapItem<Node> {

    public bool walkable;
    public Vector2 worldPosition;
    public int gridx,gridy;
    public Node parent;
    public int G_cost;
    public int H_cost;
    int heapIndex;
    public int penaltyValue;


    public Node(bool _walkable,Vector2 _worldPos,int _gridx,int _gridy,int _penalty)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridx = _gridx;
        gridy = _gridy;
        penaltyValue = _penalty;
    }

    public int F_Cost
    {
        get
        {
            return G_cost + H_cost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compare = F_Cost.CompareTo(other.F_Cost);

        if(compare == 0)
        {
            compare = H_cost.CompareTo(other.H_cost);
        }
        return -compare;
    }
}
