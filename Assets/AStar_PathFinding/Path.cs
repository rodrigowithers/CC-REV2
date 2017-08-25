using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector2[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;

    public Path(Vector2[] a , Vector2 startpos , float turndist)
    {
        lookPoints = a;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 previous = startpos;

        for(int i = 0;i<lookPoints.Length;i++)
        {
            Vector2 current = lookPoints[i];
            Vector2 dirToCurrent = (current - previous).normalized;
            Vector2 turnboundarypoint = (i == finishLineIndex)?current:current - dirToCurrent * turndist;
            turnBoundaries[i] = new Line(turnboundarypoint,previous - dirToCurrent *turndist);
            previous = turnboundarypoint;

        }

    }


    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector2 p in lookPoints)
        {
            Gizmos.DrawCube(p, Vector3.one * 5f);
        }

        Gizmos.color = Color.gray;
        foreach (Line l in turnBoundaries)
        {
            l.DrawWithGizmos(10);
        }

    }


    public Vector2[] GetLookPoints()
    {
        return lookPoints;
    }
    public Line[] GetTurnBoundaries()
    {
        return turnBoundaries;
    }
}
